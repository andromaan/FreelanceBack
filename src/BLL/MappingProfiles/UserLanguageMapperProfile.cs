using AutoMapper;
using BLL.ViewModels.UserLanguage;
using Domain.Models.Users;

namespace BLL.MappingProfiles;

public class UserLanguageMapperProfile : Profile
{
    public UserLanguageMapperProfile()
    {
        CreateMap<UserLanguage, CreateUserLanguageVM>().ReverseMap();
        CreateMap<UserLanguage, UpdateUserLanguageVM>().ReverseMap();
        CreateMap<UserLanguage, UserLanguageVM>().ReverseMap();
    }
}