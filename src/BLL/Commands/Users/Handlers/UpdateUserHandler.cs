using BLL.Common.Handlers;
using BLL.Common.Interfaces.Repositories.Countries;
using BLL.Services;
using BLL.ViewModels.User;
using Domain.Models.Users;

namespace BLL.Commands.Users.Handlers;

public class UpdateUserHandler(ICountryQueries countryQueries) : IUpdateHandler<User, UpdateUserVM>
{
    public async Task<ServiceResponse?> HandleAsync(User existingEntity, UpdateUserVM updateModel,
        CancellationToken cancellationToken)
    {
        if (await countryQueries.GetByIdAsync(updateModel.CountryId, cancellationToken) == null)
        {
            return ServiceResponse.NotFound($"Country with id {updateModel.CountryId} not found");
        }
        
        return ServiceResponse.Ok();
    }
}