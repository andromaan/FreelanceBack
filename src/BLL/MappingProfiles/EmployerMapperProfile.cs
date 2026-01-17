using AutoMapper;
using Domain.Models.Employers;
using Domain.ViewModels.Employer;

namespace BLL.MappingProfiles;

public class EmployerMapperProfile : Profile
{
    public EmployerMapperProfile()
    {
        CreateMap<UpdateEmployerVM, Employer>().ReverseMap();
        CreateMap<EmployerVM, Employer>().ReverseMap();
    }
}