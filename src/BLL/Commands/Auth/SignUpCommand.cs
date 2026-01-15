using AutoMapper;
using BLL.Common.Interfaces.Repositories;
using BLL.Common.Interfaces.Repositories.UserProfiles;
using BLL.Common.Interfaces.Repositories.Users;
using BLL.Services;
using BLL.Services.JwtService;
using BLL.Services.PasswordHasher;
using Domain;
using Domain.Models.Auth.Users;
using Domain.ViewModels.Auth;
using MediatR;

namespace BLL.Commands.Auth;

public record SignUpCommand(SignUpVm Vm) : IRequest<ServiceResponse>;

public class SignUpCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IJwtTokenService jwtTokenService,
    IUserQueries userQueries,
    IMapper mapper,
    IUserProfileRepository userProfileRepository) : IRequestHandler<SignUpCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var vm = request.Vm;
        if (!await userQueries.IsUniqueEmailAsync(vm.Email, cancellationToken))
        {
            return ServiceResponse.BadRequestResponse($"{vm.Email} вже використовується");
        }

        var isDbHasUsers = (await userQueries.GetAllAsync(cancellationToken)).Count()! != 0;

        var user = mapper.Map<User>(vm);
        user.Id = Guid.NewGuid();
        user.PasswordHash = passwordHasher.HashPassword(vm.Password);
        user.CreatedBy = user.Id;
        user.RoleId = isDbHasUsers
            ? (vm.IsFreelancer ? Settings.Roles.FreelancerRole : Settings.Roles.ClientRole)
            : Settings.Roles.AdminRole;

        try
        {
            await userRepository.CreateAsync(user, cancellationToken);

            if (isDbHasUsers && vm.IsFreelancer)
            {
                var userProfile = new UserProfile
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                };

                await userProfileRepository.CreateAsync(userProfile, user.Id, cancellationToken);
            }
        }
        catch (Exception e)
        {
            return ServiceResponse.InternalServerErrorResponse(e.Message, e.InnerException?.Message);
        }

        var tokens = await jwtTokenService.GenerateTokensAsync(user, cancellationToken);

        return ServiceResponse.OkResponse($"Користувач {vm.Email} успішно зареєстрований", tokens);
    }
}