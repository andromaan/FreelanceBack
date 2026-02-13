using AutoMapper;
using BLL.ViewModels.Auth;
using BLL.ViewModels.User;
using Domain.Models.Users;

namespace BLL.MappingProfiles;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<SignUpVM, User>().ReverseMap();

        CreateMap<User, UserVM>().ReverseMap();
        CreateMap<CreateUserVM, User>()
            .ForSourceMember(src => src.Password, opt => opt.DoNotValidate());
        CreateMap<UpdateUserVM, User>()
            .ForSourceMember(src => src.Password, opt => opt.DoNotValidate())
            .ForSourceMember(src => src.Email, opt => opt.DoNotValidate());
    }
}