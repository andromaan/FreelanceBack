using BLL.Common.Handlers;
using BLL.Common.Interfaces.Repositories.Countries;
using BLL.Services;
using BLL.ViewModels.Freelancer;
using Domain.Models.Freelance;

namespace BLL.Commands.Freelancers.Handlers;

public class UpdateFreelancerHandler(ICountryQueries countryQueries) : IUpdateHandler<Freelancer, UpdateFreelancerVM>
{
    public async Task<ServiceResponse?> HandleAsync(Freelancer existingEntity, UpdateFreelancerVM updateModel,
        CancellationToken cancellationToken)
    {
        if (await countryQueries.GetByIdAsync(updateModel.CountryId, cancellationToken) == null)
        {
            return ServiceResponse.NotFound($"Country with id {updateModel.CountryId} not found");
        }
        
        return ServiceResponse.Ok();
    }
}