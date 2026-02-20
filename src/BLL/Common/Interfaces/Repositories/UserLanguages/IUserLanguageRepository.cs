using Domain.Models.Users;

namespace BLL.Common.Interfaces.Repositories.UserLanguages;

public interface IUserLanguageRepository
{
    Task<UserLanguage?> CreateAsync(UserLanguage entity, CancellationToken token);
    Task<UserLanguage?> UpdateAsync(UserLanguage entity, CancellationToken token);
    Task<UserLanguage?> DeleteAsync(int languageId, Guid userId, CancellationToken token);
}