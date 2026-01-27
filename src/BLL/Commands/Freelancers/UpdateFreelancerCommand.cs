using AutoMapper;
using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Countries;
using BLL.Common.Interfaces.Repositories.Freelancers;
using BLL.Common.Interfaces.Repositories.Users;
using BLL.Services;
using BLL.ViewModels.Freelancer;
using MediatR;

namespace BLL.Commands.Freelancers;

public record UpdateFreelancerCommand(UpdateFreelancerVM Vm) : IRequest<ServiceResponse>;

public class UpdateFreelancerCommandHandler(
    IUserQueries userQueries,
    IFreelancerRepository freelancerRepository,
    IFreelancerQueries freelancerQueries,
    ICountryQueries countryQueries,
    IMapper mapper,
    IUserProvider userProvider)
    : IRequestHandler<UpdateFreelancerCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(UpdateFreelancerCommand request, CancellationToken cancellationToken)
    {
        var userId = await userProvider.GetUserId();
        var vm = request.Vm;

        var existingUser = await userQueries.GetByIdAsync(userId, cancellationToken);
        if (existingUser == null)
        {
            return ServiceResponse.NotFound($"User with id {userId} not found");
        }
        
        var existingFreelancer = await freelancerQueries.GetByUserIdAsync(userId, cancellationToken);
        if (existingFreelancer == null)
        {
            return ServiceResponse.NotFound($"User with id {userId} is not a freelancer");
        }

        if (await countryQueries.GetByIdAsync(vm.CountryId, cancellationToken) == null)
        {
            return ServiceResponse.NotFound($"Country with id {vm.CountryId} not found");
        }
        
        var updatedProfile = mapper.Map(vm, existingFreelancer);
        
        try
        {
            await freelancerRepository.UpdateAsync(updatedProfile, cancellationToken);
            return ServiceResponse.Ok("User profile updated successfully", updatedProfile);
        }
        catch (Exception exception)
        {
            return ServiceResponse.InternalError(exception.Message);
        }
    }
}