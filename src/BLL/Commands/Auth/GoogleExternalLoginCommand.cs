using BLL.Common.Interfaces.Repositories.Users;
using BLL.Services;
using BLL.Services.JwtService;
using BLL.Services.PasswordHasher;
using BLL.ViewModels.Auth;
using Domain;
using Domain.Models.Users;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BLL.Commands.Auth;

public record GoogleExternalLoginCommand : IRequest<ServiceResponse>
{
    public required ExternalLoginVM Model { get; init; }
}

public class GoogleExternalLoginCommandHandler(
    IUserRepository userRepository,
    IUserQueries userQueries,
    IJwtTokenService jwtTokenService,
    IPasswordHasher hashPasswordService)
    : IRequestHandler<GoogleExternalLoginCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(GoogleExternalLoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Model.Token))
                return ServiceResponse.BadRequest("Google token not sent");

            var payload = await jwtTokenService.VerifyGoogleToken(request.Model);

            var info = new UserLoginInfo(request.Model.Provider, payload.Subject, request.Model.Provider);

            // var isUsersNullOrEmpty = (await userQueries.GetAllAsync(cancellationToken)).Any();

            var user = await FindOrCreateUserAsync(payload, info, cancellationToken);

            if (user is null)
                return ServiceResponse.BadRequest("Failed to add Google login");

            var tokens = await jwtTokenService.GenerateTokensAsync(user, cancellationToken);
            return ServiceResponse.Ok("Users tokens", tokens);
        }
        catch (Exception ex)
        {
            return ServiceResponse.InternalError(ex.Message);
        }
    }

    private async Task<User?> FindOrCreateUserAsync(GoogleJsonWebSignature.Payload payload,
        UserLoginInfo info, CancellationToken cancellationToken)
    {
        var user = await userQueries.FindByLoginAsync(info.LoginProvider, info.ProviderKey, cancellationToken);
        if (user != null)
            return user;

        user = await userQueries.GetByEmailAsync(payload.Email, cancellationToken);
        if (user == null)
        {
            user = await CreateUserAsync(payload, cancellationToken);
        }

        var loginResult = await userRepository.AddLoginAsync(user, info, cancellationToken);
        return loginResult.Succeeded ? user : null;
    }

    private async Task<User> CreateUserAsync(GoogleJsonWebSignature.Payload payload,
        CancellationToken cancellationToken)
    {
        var userId = Guid.NewGuid();
        var randomPassword = GenerateRandomPassword();

        // var (name, patronymic) = SplitFullName(payload.Name);

        var userModel = new User
        {
            Id = userId,
            Email = payload.Email,
            RoleId = Settings.Roles.AdminRole,
            PasswordHash = hashPasswordService.HashPassword(randomPassword)
        };

        var result = await userRepository.CreateAsync(userModel, cancellationToken);

        if (result is null)
        {
            throw new Exception("Failed to create user");
        }

        return result;
    }

    // private (string? name, string? patronymic) SplitFullName(string? fullName)
    // {
    //     if (string.IsNullOrWhiteSpace(fullName))
    //         return (null, null);
    //
    //     var parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    //     var name = parts.ElementAtOrDefault(0);
    //     var patronymic = parts.ElementAtOrDefault(1);
    //
    //     return (name, patronymic);
    // }

    private string GenerateRandomPassword()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 6)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}