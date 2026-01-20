using AutoMapper;
using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Freelancers;
using BLL.Common.Interfaces.Repositories.Languages;
using BLL.Common.Interfaces.Repositories.Users;
using BLL.Services;
using BLL.ViewModels.Freelancer;
using Domain.Common.Interfaces;
using MediatR;

namespace BLL.Commands.Freelancers;

public record UpdateFrInfoLangCommand(UpdateFrInfoLangVM Vm) : IRequest<ServiceResponse>
{
}

public class UpdateFrInfoLangCommandHandler(
    IUserProvider userProvider,
    IMapper mapper,
    IFreelancerQueries freelancerQueries,
    IFreelancerRepository freelancerRepository,
    ILanguageQueries languageQueries
) : IRequestHandler<UpdateFrInfoLangCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(UpdateFrInfoLangCommand request, CancellationToken cancellationToken)
    {
        var vm = request.Vm;
        var userId = await userProvider.GetUserId();

        var existingFreelancer = await freelancerQueries.GetByUserId(userId, cancellationToken, true);
        if (existingFreelancer == null)
        {
            return ServiceResponse.NotFound($"User with id {userId} does not have a profile");
        }
        
        existingFreelancer.Languages.Clear();

        foreach (var langId in vm.LanguageIds)
        {
            var existingLanguage = await languageQueries.GetByIdAsync(langId, cancellationToken);
            if (existingLanguage == null)
            {
                return ServiceResponse.NotFound($"Language with id {langId} not found");
            }

            existingFreelancer.Languages.Add(existingLanguage);
        }

        try
        {
            await freelancerRepository.UpdateAsync(existingFreelancer, cancellationToken);
            return ServiceResponse.Ok("User profile languages updated successfully",
                mapper.Map<FreelancerVM>(existingFreelancer));
        }
        catch (Exception exception)
        {
            return ServiceResponse.InternalError(exception.Message);
        }
    }
}