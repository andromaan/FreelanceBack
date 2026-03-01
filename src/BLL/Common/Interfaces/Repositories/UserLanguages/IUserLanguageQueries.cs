using Domain.Models.Users;

namespace BLL.Common.Interfaces.Repositories.UserLanguages;

public interface IUserLanguageQueries
{
    Task<UserLanguage?> GetByIdAsync(int languageId, Guid userId, CancellationToken token);
}