using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Languages;
using BLL.Common.Interfaces.Repositories.UserLanguages;
using BLL.Services;
using MediatR;

namespace BLL.CommandsQueries.UserLanguages;

public record DeleteUserLanguageCommand(int LanguageId) : IRequest<ServiceResponse>;

public class DeleteUserLanguageCommandHandler(
    IUserProvider userProvider,
    IUserLanguageRepository userLanguageRepository,
    IUserLanguageQueries userLanguageQueries,
    ILanguageQueries languageQueries) : IRequestHandler<DeleteUserLanguageCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(DeleteUserLanguageCommand request, CancellationToken cancellationToken)
    {
        var language = await languageQueries.GetByIdAsync(request.LanguageId, cancellationToken);
        if (language == null)
        {
            return ServiceResponse.NotFound("Language not found");
        }

        try
        {
            var existingUserLanguage = await userLanguageQueries.GetByIdAsync(request.LanguageId,
                await userProvider.GetUserId(cancellationToken), cancellationToken);

            if (existingUserLanguage == null)
            {
                return ServiceResponse.NotFound("User language not found");
            }

            await userLanguageRepository.DeleteAsync(existingUserLanguage.LanguageId, existingUserLanguage.UserId,
                cancellationToken);

            return ServiceResponse.Ok("User language deleted");
        }
        catch (Exception exception)
        {
            return ServiceResponse.InternalError(exception.Message);
        }
    }
}