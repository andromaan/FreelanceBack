using AutoMapper;
using BLL.ViewModels.Language;
using Domain.Models.Languages;

namespace BLL.MappingProfiles;

public class LanguageMapperProfile : Profile
{
    public LanguageMapperProfile()
    {
        CreateMap<Language, LanguageVM>().ReverseMap();
        CreateMap<Language, CreateLanguageVM>().ReverseMap();
        CreateMap<Language, UpdateLanguageVM>().ReverseMap();
    }
}