using BLL.Common.Interfaces.Repositories.UserLanguages;
using DAL.Data;
using Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class UserLanguageRepository(AppDbContext appDbContext) : IUserLanguageRepository, IUserLanguageQueries
{
    public async Task<UserLanguage?> CreateAsync(UserLanguage entity, CancellationToken token)
    {
        await appDbContext.Set<UserLanguage>().AddAsync(entity, token);
        await SaveAsync(token);
        return entity;
    }

    public async Task<UserLanguage?> UpdateAsync(UserLanguage entity, CancellationToken token)
    {
        appDbContext.Set<UserLanguage>().Update(entity);
        await SaveAsync(token);
        return entity;
    }

    public async Task<UserLanguage?> DeleteAsync(int languageId, Guid userId, CancellationToken token)
    {
        var entity = await appDbContext.Set<UserLanguage>()
            .FirstOrDefaultAsync(ul => ul.LanguageId == languageId && ul.UserId == userId, token);

        if (entity == null)
            return null;

        appDbContext.Set<UserLanguage>().Remove(entity);
        await SaveAsync(token);
        return entity;
    }

    public async Task<UserLanguage?> GetByIdAsync(int languageId, Guid userId, CancellationToken token)
    {
        return await appDbContext.Set<UserLanguage>()
            .FirstOrDefaultAsync(ul => ul.LanguageId == languageId && ul.UserId == userId, token);
    }
    
    private async Task SaveAsync(CancellationToken token) => await appDbContext.SaveChangesAsync(token);
}