using AutoMapper;
using BLL.Common.Interfaces.Repositories.Employers;
using BLL.Common.Interfaces.Repositories.Freelancers;
using BLL.Common.Interfaces.Repositories.Users;
using BLL.Common.Interfaces.Repositories.UserWallets;
using BLL.Services;
using BLL.Services.JwtService;
using BLL.Services.PasswordHasher;
using BLL.ViewModels.Auth;
using Domain.Models.Employers;
using Domain.Models.Freelance;
using Domain.Models.Payments;
using Domain.Models.Users;
using MediatR;

namespace BLL.Commands.Auth;

public record SignUpCommand(SignUpVM Vm) : IRequest<ServiceResponse>;

public class SignUpCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IJwtTokenService jwtTokenService,
    IUserQueries userQueries,
    IMapper mapper,
    IFreelancerRepository freelancerRepository,
    IEmployerRepository employerRepository,
    IUserWalletRepository userWalletRepository) : IRequestHandler<SignUpCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var vm = request.Vm;
        if (!await userQueries.IsUniqueEmailAsync(vm.Email, cancellationToken))
        {
            return ServiceResponse.BadRequest($"{vm.Email} already exists");
        }
        
        if (vm is not {UserRole: Settings.Roles.EmployerRole or Settings.Roles.FreelancerRole})
        {
            return ServiceResponse.BadRequest(
                $"Invalid user role, must be '{Settings.Roles.EmployerRole}' or '{Settings.Roles.FreelancerRole}'");
        }

        var isDbHasUsers = (await userQueries.GetAllAsync(cancellationToken)).Count() != 0;
        var userRole = isDbHasUsers ? vm.UserRole : Settings.Roles.AdminRole;


        var user = mapper.Map<User>(vm);
        user.Id = Guid.NewGuid();
        user.PasswordHash = passwordHasher.HashPassword(vm.Password);
        user.CreatedBy = user.Id;
        user.RoleId = userRole;

        try
        {
            await userRepository.CreateAsync(user, cancellationToken);

            if (user.RoleId == Settings.Roles.FreelancerRole)
            {
                var freelancer = new Freelancer
                {
                    Id = Guid.NewGuid(),
                    CreatedBy = user.Id,
                };

                await freelancerRepository.CreateAsync(freelancer, user.Id, cancellationToken);
            }

            if (user.RoleId == Settings.Roles.EmployerRole)
            {
                var employer = new Employer
                {
                    Id = Guid.NewGuid(),
                    CreatedBy = user.Id,
                };

                await employerRepository.CreateAsync(employer, user.Id, cancellationToken);
            }

            if (user.RoleId != Settings.Roles.AdminRole)
            {
                var userWallet = new UserWallet
                {
                    Id = Guid.NewGuid(),
                    CreatedBy = user.Id,
                    Balance = 0m
                };

                await userWalletRepository.CreateAsync(userWallet, cancellationToken);
            }
        }
        catch (Exception e)
        {
            return ServiceResponse.InternalError(e.Message, e.InnerException?.Message);
        }

        var tokens = await jwtTokenService.GenerateTokensAsync(user, cancellationToken);

        return ServiceResponse.Ok($"User {vm.Email} successfully created", tokens);
    }
}