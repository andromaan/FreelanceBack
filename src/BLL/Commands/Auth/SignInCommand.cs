using BLL.Common.Interfaces.Repositories;
using BLL.Common.Interfaces.Repositories.Users;
using BLL.Services;
using BLL.Services.JwtService;
using BLL.Services.PasswordHasher;
using Domain.ViewModels.Auth;
using MediatR;

namespace BLL.Commands.Auth;

public record SignInCommand(SignInVm Vm) : IRequest<ServiceResponse>;

public class SignInCommandHandler(
    IUserRepository userRepository,
    IUserQueries userQueries,
    IPasswordHasher passwordHasher,
    IJwtTokenService jwtService) : IRequestHandler<SignInCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var vm = request.Vm;
        
        var user = await userQueries.GetByEmailAsync(vm.Email, cancellationToken);

        if (user == null)
        {
            return ServiceResponse.BadRequestResponse($"Користувача з поштою {vm.Email} не знайдено");
        }

        var result = passwordHasher.Verify(vm.Password, user.PasswordHash);

        if (!result)
        {
            return ServiceResponse.BadRequestResponse($"Пароль вказано невірно");
        }

        var tokens = await jwtService.GenerateTokensAsync(user, cancellationToken);

        return ServiceResponse.OkResponse("Успішний вхід", tokens);
    }
}