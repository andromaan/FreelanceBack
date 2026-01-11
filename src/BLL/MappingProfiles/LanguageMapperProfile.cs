using AutoMapper;
using Domain.Models.Languages;
using Domain.ViewModels.Language;

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