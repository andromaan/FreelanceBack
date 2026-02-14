using BLL.Common.Handlers;
using BLL.Common.Interfaces.Repositories.Languages;
using BLL.Services;
using BLL.ViewModels.Freelancer;
using Domain.Models.Freelance;

namespace BLL.Commands.Freelancers.Handlers;

public class UpdateFreelancerLanguagesHandler(ILanguageQueries languageQueries) : IUpdateHandler<Freelancer, UpdateFreelancerLanguagesVM>
{
    public async Task<ServiceResponse?> HandleAsync(Freelancer existingEntity, UpdateFreelancerLanguagesVM updateModel,
        CancellationToken cancellationToken)
    {
        existingEntity.Languages.Clear();

        foreach (var langId in updateModel.LanguageIds.Distinct())
        {
            var existingLanguage = await languageQueries.GetByIdAsync(langId, cancellationToken);
            if (existingLanguage == null)
            {
                return ServiceResponse.NotFound($"Language with id {langId} not found");
            }

            existingEntity.Languages.Add(existingLanguage);
        }
        
        return ServiceResponse.Ok();
    }
}