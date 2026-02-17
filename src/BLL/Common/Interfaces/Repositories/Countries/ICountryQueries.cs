using Domain.Models.Countries;

namespace BLL.Common.Interfaces.Repositories.Countries;

public interface ICountryQueries : IQueries<Country, int>, IUniqueQuery<Country, int>
{
    
}