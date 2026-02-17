using AutoMapper;
using BLL.ViewModels.Country;
using Domain.Models.Countries;

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