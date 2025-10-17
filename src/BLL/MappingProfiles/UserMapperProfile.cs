using AutoMapper;
using Domain.Models.Auth.Users;
using Domain.ViewModels.Auth;

namespace BLL.MappingProfiles;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<SignUpVm, User>().ReverseMap();
    }
}