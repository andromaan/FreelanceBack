using BLL.Common.Handlers;
using BLL.Common.Interfaces.Repositories.Languages;
using BLL.Services;
using BLL.ViewModels.User;
using Domain.Models.Users;

namespace BLL.Commands.Users.Handlers;

public class UpdateUserLanguagesHandler(ILanguageQueries languageQueries) : IUpdateHandler<User, UpdateUserLanguagesVM>
{
    public async Task<ServiceResponse?> HandleAsync(User existingEntity, UpdateUserLanguagesVM updateModel,
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