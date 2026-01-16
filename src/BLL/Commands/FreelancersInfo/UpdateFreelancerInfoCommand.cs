using AutoMapper;
using BLL.Common.Interfaces.Repositories.Countries;
using BLL.Common.Interfaces.Repositories.FreelancersInfo;
using BLL.Common.Interfaces.Repositories.Users;
using BLL.Services;
using Domain.Common.Interfaces;
using Domain.ViewModels.FreelancerInfo;
using MediatR;

namespace BLL.Commands.FreelancersInfo;

public record UpdateFreelancerInfoCommand(UpdateFreelancerInfoVM Vm) : IRequest<ServiceResponse>
{
}

public class UpdateFreelancerInfoCommandHandler(
    IUserQueries userQueries,
    IFreelancerInfoRepository freelancerInfoRepository,
    IFreelancerInfoQueries freelancerInfoQueries,
    ICountryQueries countryQueries,
    IMapper mapper,
    IUserProvider userProvider)
    : IRequestHandler<UpdateFreelancerInfoCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(UpdateFreelancerInfoCommand request, CancellationToken cancellationToken)
    {
        var userId = await userProvider.GetUserId();
        var vm = request.Vm;

        var existingUser = await userQueries.GetByIdAsync(userId, cancellationToken);
        if (existingUser == null)
        {
            return ServiceResponse.NotFoundResponse($"User with id {userId} not found");
        }
        
        var existingFreelancerInfo = await freelancerInfoQueries.GetByUserId(userId, cancellationToken);
        if (existingFreelancerInfo == null)
        {
            return ServiceResponse.NotFoundResponse($"User with id {userId} does not have a profile");
        }

        if (await countryQueries.GetByIdAsync(vm.CountryId, cancellationToken) == null)
        {
            return ServiceResponse.NotFoundResponse($"Country with id {vm.CountryId} not found");
        }
        
        var updatedProfile = mapper.Map(vm, existingFreelancerInfo);
        
        try
        {
            await freelancerInfoRepository.UpdateAsync(updatedProfile, cancellationToken);
            return ServiceResponse.OkResponse("User profile updated successfully", updatedProfile);
        }
        catch (Exception exception)
        {
            return ServiceResponse.InternalServerErrorResponse(exception.Message);
        }
    }
}