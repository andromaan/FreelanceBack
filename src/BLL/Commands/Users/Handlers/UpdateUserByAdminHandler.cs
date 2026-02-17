using BLL.Common.Handlers;
using BLL.Common.Interfaces.Repositories.Countries;
using BLL.Common.Interfaces.Repositories.Users;
using BLL.Services;
using BLL.Services.PasswordHasher;
using BLL.ViewModels.User;
using Domain.Models.Users;

namespace BLL.Commands.Users.Handlers;

public class UpdateUserByAdminHandler(
    IPasswordHasher passwordHasher,
    IUserQueries userQueries,
    ICountryQueries countryQueries)
    : IUpdateHandler<User, UpdateUserByAdminVM>
{
    public async Task<ServiceResponse?> HandleAsync(User existingEntity, UpdateUserByAdminVM updateModel,
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

        if (updateModel.DisplayName != null)
        {
            existingEntity.DisplayName = updateModel.DisplayName;
        }

        if (updateModel.CountryId != null)
        {
            if (await countryQueries.GetByIdAsync((int)updateModel.CountryId, cancellationToken) == null)
            {
                return ServiceResponse.NotFound($"Country with id {updateModel.CountryId} not found");
            }

            existingEntity.CountryId = updateModel.CountryId;
        }

        return ServiceResponse.Ok();
    }
}