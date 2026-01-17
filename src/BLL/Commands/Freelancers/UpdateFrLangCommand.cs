using AutoMapper;
using BLL.Common.Interfaces.Repositories.Freelancers;
using BLL.Common.Interfaces.Repositories.Languages;
using BLL.Common.Interfaces.Repositories.Users;
using BLL.Services;
using Domain.Common.Interfaces;
using Domain.ViewModels.Freelancer;
using MediatR;

namespace BLL.Commands.Freelancers;

public record UpdateFrInfoLangCommand(UpdateFrInfoLangVM Vm) : IRequest<ServiceResponse>
{
}

public class UpdateFrInfoLangCommandHandler(
    IUserProvider userProvider,
    IMapper mapper,
    IUserQueries userQueries,
    IFreelancerQueries freelancerQueries,
    IFreelancerRepository freelancerRepository,
    ILanguageQueries languageQueries
    ) : IRequestHandler<UpdateFrInfoLangCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(UpdateFrInfoLangCommand request, CancellationToken cancellationToken)
    {
        var vm = request.Vm;
        var userId = await userProvider.GetUserId();
        
        if (await userQueries.GetByIdAsync(userId, cancellationToken) == null)
        {
            return ServiceResponse.NotFoundResponse($"User with id {userId} not found");
        }
        
        var existingFreelancer = await freelancerQueries.GetByUserId(userId, cancellationToken, true);
        if (existingFreelancer == null)
        {
            return ServiceResponse.NotFoundResponse($"User with id {userId} does not have a profile");
        }

        foreach (var langId in vm.LanguageIds)
        {
            var existingLanguage = await languageQueries.GetByIdAsync(langId, cancellationToken);  
            if (existingLanguage == null)
            {
                return ServiceResponse.NotFoundResponse($"Language with id {langId} not found");
            }
            
            existingFreelancer.Languages.Add(existingLanguage);
        }
        
        try
        {
            await freelancerRepository.UpdateAsync(existingFreelancer, cancellationToken);
            return ServiceResponse.OkResponse("User profile languages updated successfully", existingFreelancer);
        }
        catch (Exception exception)
        {
            return ServiceResponse.InternalServerErrorResponse(exception.Message);
        }
    }
}
