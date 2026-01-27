using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Languages;
using DAL.Data;
using Domain.Models.Languages;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class LanguageRepository(AppDbContext appDbContext, IUserProvider userProvider)
    : Repository<Language, int>(appDbContext, userProvider), ILanguageRepository, ILanguageQueries
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<bool> IsUniqueAsync(Language entity, CancellationToken token)
    {
        return await _appDbContext.Set<Language>()
            .FirstOrDefaultAsync(c => c.Name == entity.Name || c.Code == entity.Code && c.Id != entity.Id, token) == null;
    }
}

