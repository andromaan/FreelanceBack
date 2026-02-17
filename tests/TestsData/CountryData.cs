using Domain.Models.Countries;

namespace TestsData;

public class CountryData
{
    public static Country MainCountry => new()
    {
        Id = 0,
        Name = "MainCountry",
        Alpha2Code = "MC",
        Alpha3Code = "MCN",
    };
}
