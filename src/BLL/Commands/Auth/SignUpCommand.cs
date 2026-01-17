using AutoMapper;
using BLL.Common.Interfaces.Repositories.Employers;
using BLL.Common.Interfaces.Repositories.Freelancers;
using BLL.Common.Interfaces.Repositories.Users;
using BLL.Services;
using BLL.Services.JwtService;
using BLL.Services.PasswordHasher;
using Domain;
using Domain.Models.Auth.Users;
using Domain.Models.Employers;
using Domain.Models.Freelance;
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
    IFreelancerRepository FreelancerRepository,
    IEmployerRepository employerRepository) : IRequestHandler<SignUpCommand, ServiceResponse>
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
            ? (vm.IsFreelancer ? Settings.Roles.FreelancerRole : Settings.Roles.EmployerRole)
            : Settings.Roles.AdminRole;

        try
        {
            await userRepository.CreateAsync(user, cancellationToken);

            if (user.RoleId == Settings.Roles.FreelancerRole)
            {
                var freelancer = new Freelancer
                {
                    Id = user.Id,
                    UserId = user.Id,
                };

                await FreelancerRepository.CreateAsync(freelancer, user.Id, cancellationToken);
            }

            if (user.RoleId == Settings.Roles.EmployerRole)
            {
                var employer = new Employer
                {
                    Id = user.Id,
                    UserId = user.Id,
                };

                await employerRepository.CreateAsync(employer, user.Id, cancellationToken);
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