using AutoMapper;
using BLL.ViewModels.Roles;
using Domain.Models.Auth;

namespace BLL.MappingProfiles;

public class RoleMapperProfile : Profile
{
    public RoleMapperProfile()
    {
        CreateMap<Role, RoleVM>().ReverseMap();
    }
}