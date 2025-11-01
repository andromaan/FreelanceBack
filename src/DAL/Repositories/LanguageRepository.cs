using BLL.Common.Interfaces.Repositories;
using DAL.Data;
using Domain.Common.Interfaces;
using Domain.Models.Languages;

namespace DAL.Repositories;

public class LanguageRepository(AppDbContext appDbContext, IUserProvider userProvider)
    : Repository<Language, int>(appDbContext, userProvider), ILanguageRepository
{
    
}

