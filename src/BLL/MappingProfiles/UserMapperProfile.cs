using AutoMapper;
using BLL.ViewModels.Auth;
using Domain.Models.Users;

namespace BLL.MappingProfiles;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<SignUpVM, User>().ReverseMap();
    }
}