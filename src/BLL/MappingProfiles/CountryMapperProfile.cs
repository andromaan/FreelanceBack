using AutoMapper;
using Domain.Models.Countries;
using Domain.ViewModels.Country;

namespace BLL.MappingProfiles;

public class CountryMapperProfile : Profile
{
    public CountryMapperProfile()
    {
        CreateMap<Country, CreateCountryVM>().ReverseMap();
        CreateMap<Country, UpdateCountryVM>().ReverseMap();
        CreateMap<Country, CountryVM>().ReverseMap();
    }
}