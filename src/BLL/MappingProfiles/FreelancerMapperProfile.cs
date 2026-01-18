using AutoMapper;
using BLL.ViewModels.Freelancer;
using Domain.Models.Freelance;

namespace BLL.MappingProfiles;

public class FreelancerMapperProfile : Profile
{
    public FreelancerMapperProfile()
    {
        CreateMap<Freelancer, UpdateFreelancerVM>().ReverseMap();
        CreateMap<Freelancer, FreelancerVM>().ReverseMap();
    }
}