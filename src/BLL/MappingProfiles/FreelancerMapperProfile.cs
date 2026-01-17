using AutoMapper;
using Domain.Models.Freelance;
using Domain.ViewModels.Freelancer;

namespace BLL.MappingProfiles;

public class FreelancerMapperProfile : Profile
{
    public FreelancerMapperProfile()
    {
        CreateMap<Freelancer, UpdateFreelancerVM>().ReverseMap();
        CreateMap<Freelancer, FreelancerVM>().ReverseMap();
    }
}