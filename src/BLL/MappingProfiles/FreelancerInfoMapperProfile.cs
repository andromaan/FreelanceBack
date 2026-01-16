using AutoMapper;
using Domain.Models.Freelance;
using Domain.ViewModels.FreelancerInfo;

namespace BLL.MappingProfiles;

public class FreelancerInfoMapperProfile : Profile
{
    public FreelancerInfoMapperProfile()
    {
        CreateMap<FreelancerInfo, UpdateFreelancerInfoVM>().ReverseMap();
        CreateMap<FreelancerInfo, FreelancerInfoVM>().ReverseMap();
    }
}