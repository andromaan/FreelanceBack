using BLL.Common.Interfaces.Repositories;
using BLL.Services;
using BLL.Services.JwtService;
using BLL.Services.PasswordHasher;
using Domain;
using Domain.Models.Auth.Users;
using Domain.ViewModels;
using Domain.ViewModels.Auth;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BLL.Commands.Auth;

public record GoogleExternalLoginCommand : IRequest<ServiceResponse>
{
    public required ExternalLoginVm Model { get; init; }
}

public class GoogleExternalLoginCommandHandler(
    IUserRepository userRepository,
    IJwtTokenService jwtTokenService,
    IPasswordHasher hashPasswordService)
    : IRequestHandler<GoogleExternalLoginCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(GoogleExternalLoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Model == null || string.IsNullOrEmpty(request.Model.Token))
                return ServiceResponse.BadRequestResponse("Google token not sent");

            var payload = await jwtTokenService.VerifyGoogleToken(request.Model);

            if (payload is null)
                return ServiceResponse.BadRequestResponse("Invalid Google token");

            var info = new UserLoginInfo(request.Model.Provider, payload.Subject, request.Model.Provider);

            var isUsersNullOrEmpty = (await userRepository.GetAllAsync(cancellationToken)).Any();

            var user = await FindOrCreateUserAsync(payload, info, isUsersNullOrEmpty, cancellationToken);

            if (user is null)
                return ServiceResponse.BadRequestResponse("Failed to add Google login");

            var tokens = await jwtTokenService.GenerateTokensAsync(user, cancellationToken);
            return ServiceResponse.OkResponse("Users tokens", tokens);
        }
        catch (Exception ex)
        {
            return ServiceResponse.InternalServerErrorResponse(ex.Message);
        }
    }

    private async Task<User?> FindOrCreateUserAsync(GoogleJsonWebSignature.Payload payload,
        UserLoginInfo info, bool isUsersNullOrEmpty, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByLoginAsync(info.LoginProvider, info.ProviderKey, cancellationToken);
        if (user != null)
            return user;

        user = await userRepository.GetByEmailAsync(payload.Email, cancellationToken);
        if (user == null)
        {
            user = await CreateUserAsync(payload, isUsersNullOrEmpty, cancellationToken);
        }

        var loginResult = await userRepository.AddLoginAsync(user, info, cancellationToken);
        return loginResult.Succeeded ? user : null;
    }

    private async Task<User> CreateUserAsync(GoogleJsonWebSignature.Payload payload,
        bool isUsersNullOrEmpty,
        CancellationToken cancellationToken)
    {
        var userId = Guid.NewGuid();
        var randomPassword = GenerateRandomPassword();

        var (name, patronymic) = SplitFullName(payload.Name);

        var userModel = new User
        {
            Id = userId,
            Email = payload.Email,
            RoleId = isUsersNullOrEmpty
                ? Settings.Roles.AdminRole
                : Settings.Roles.AdminRole, // TODO fix roles for user
            PasswordHash = hashPasswordService.HashPassword(randomPassword)
        };

        var result = await userRepository.CreateAsync(userModel, cancellationToken);

        if (result is null)
        {
            throw new Exception("Failed to create user");
        }

        return result;
    }

    private (string? name, string? patronymic) SplitFullName(string? fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            return (null, null);

        var parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var name = parts.ElementAtOrDefault(0);
        var patronymic = parts.ElementAtOrDefault(1);

        return (name, patronymic);
    }

    private string GenerateRandomPassword()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 6)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}