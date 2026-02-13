using AutoMapper;
using BLL.Common.Interfaces.Repositories.Employers;
using BLL.Common.Interfaces.Repositories.Freelancers;
using BLL.Common.Interfaces.Repositories.Users;
using BLL.Common.Interfaces.Repositories.UserWallets;
using BLL.Services;
using BLL.Services.PasswordHasher;
using BLL.ViewModels.User;
using Domain.Models.Employers;
using Domain.Models.Freelance;
using Domain.Models.Payments;
using Domain.Models.Users;
using MediatR;

namespace BLL.Commands.Users;

public record CreateUserCommand(CreateUserVM CreateModel) : IRequest<ServiceResponse>;

public class CreateUserCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IUserQueries userQueries,
    IFreelancerRepository freelancerRepository,
    IEmployerRepository employerRepository,
    IUserWalletRepository userWalletRepository,
    IMapper mapper) : IRequestHandler<CreateUserCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<User>(request.CreateModel);
        
        var createModel = request.CreateModel;
        
        if (!Settings.Roles.ListOfRoles.Contains(createModel.RoleId))
        {
            return ServiceResponse.BadRequest("Invalid role specified. Valid roles are: " +
                                              string.Join(", ", Settings.Roles.ListOfRoles));
        }

        var userWithEmail = await userQueries.GetByEmailAsync(createModel.Email, cancellationToken);
        if (userWithEmail != null)
        {
            return ServiceResponse.BadRequest($"A user with the email {createModel.Email} already exists.");
        }

        entity.PasswordHash = passwordHasher.HashPassword(createModel.Password);
        
        try
        {
            var createdEntity = await userRepository.CreateAsync(entity, cancellationToken);
            
            await ConfigureUserBaseOfRole(entity, cancellationToken);
            
            return ServiceResponse.Ok($"User created",
                mapper.Map<UserVM>(createdEntity));
        }
        catch (Exception exception)
        {
            return ServiceResponse.InternalError(exception.Message, data: exception.InnerException?.Message );
        }
    }
    
    private async Task ConfigureUserBaseOfRole(User createdUser, CancellationToken cancellationToken)
    {
        if (createdUser.RoleId == Settings.Roles.FreelancerRole)
        {
            var freelancer = new Freelancer
            {
                Id = Guid.NewGuid(),
                CreatedBy = createdUser.Id,
            };

            await freelancerRepository.CreateAsync(freelancer, createdUser.Id, cancellationToken);
        }

        if (createdUser.RoleId == Settings.Roles.EmployerRole)
        {
            var employer = new Employer
            {
                Id = Guid.NewGuid(),
                UserId = createdUser.Id,
            };

            await employerRepository.CreateAsync(employer, createdUser.Id, cancellationToken);
        }

        if (createdUser.RoleId != Settings.Roles.AdminRole)
        {
            var userWallet = new UserWallet
            {
                Id = Guid.NewGuid(),
                CreatedBy = createdUser.Id,
                Balance = 0m
            };

            await userWalletRepository.CreateAsync(userWallet, cancellationToken);
        }
    }
}