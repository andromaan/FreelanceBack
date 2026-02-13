using BLL.Common.Handlers;
using BLL.Common.Interfaces.Repositories.Users;
using BLL.Services;
using BLL.Services.PasswordHasher;
using BLL.ViewModels.User;
using Domain.Models.Users;

namespace BLL.Commands.Users.Handlers;

public class UpdateUserHandler(IPasswordHasher passwordHasher, IUserQueries userQueries) : IUpdateHandler<User, UpdateUserVM>
{
    public async Task<ServiceResponse?> HandleAsync(User existingEntity, UpdateUserVM updateModel,
        CancellationToken cancellationToken)
    {
        if (updateModel.Email != null)
        {
            var emailExists = await userQueries.GetByEmailAsync(updateModel.Email, cancellationToken);
            if (emailExists != null && emailExists.Id != existingEntity.Id)
            {
                return ServiceResponse.BadRequest("Email is already in use.");
            }

            existingEntity.Email = updateModel.Email;
        }
        
        if (updateModel.Password != null)
        {
            existingEntity.PasswordHash = passwordHasher.HashPassword(updateModel.Password);
        }

        return ServiceResponse.Ok();
    }
}