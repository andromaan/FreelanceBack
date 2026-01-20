using BLL.Common.Interfaces.Repositories.Users;
using BLL.Services;
using BLL.Services.JwtService;
using BLL.Services.PasswordHasher;
using BLL.ViewModels.Auth;
using MediatR;

namespace BLL.Commands.Auth;

public record SignInCommand(SignInVM Vm) : IRequest<ServiceResponse>;

public class SignInCommandHandler(
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
            return ServiceResponse.BadRequest($"Користувача з поштою {vm.Email} не знайдено");
        }

        var result = passwordHasher.Verify(vm.Password, user.PasswordHash);

        if (!result)
        {
            return ServiceResponse.BadRequest($"Пароль вказано невірно");
        }

        var tokens = await jwtService.GenerateTokensAsync(user, cancellationToken);

        return ServiceResponse.Ok("Успішний вхід", tokens);
    }
}