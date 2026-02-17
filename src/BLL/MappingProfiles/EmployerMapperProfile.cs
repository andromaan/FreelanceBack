using AutoMapper;
using BLL.ViewModels.Employer;
using Domain.Models.Employers;

namespace BLL.MappingProfiles;

public class EmployerMapperProfile : Profile
{
    public EmployerMapperProfile()
    {
        CreateMap<UpdateEmployerVM, Employer>().ReverseMap();
        CreateMap<EmployerVM, Employer>().ReverseMap();
    }
}