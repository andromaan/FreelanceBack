using BLL.Common.Interfaces.Repositories.Countries;
using DAL.Data;
using Domain.Common.Interfaces;
using Domain.Models.Countries;

namespace DAL.Repositories;

public class CountryRepository(AppDbContext appDbContext, IUserProvider userProvider)
    : Repository<Country, int>(appDbContext, userProvider), ICountryRepository, ICountryQueries
{
    
}

