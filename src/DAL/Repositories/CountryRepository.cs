using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Countries;
using DAL.Data;
using Domain.Models.Countries;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class CountryRepository(AppDbContext appDbContext, IUserProvider userProvider)
    : Repository<Country, int>(appDbContext, userProvider), ICountryRepository, ICountryQueries
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<bool> IsUniqueAsync(Country entity, CancellationToken token)
    {
        return await _appDbContext.Set<Country>()
            .FirstOrDefaultAsync(c => c.Name == entity.Name && c.Id != entity.Id, token) == null;
    }
}

