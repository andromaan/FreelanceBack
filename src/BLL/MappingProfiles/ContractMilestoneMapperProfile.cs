using AutoMapper;
using BLL.ViewModels.ContractMilestone;
using Domain.Models.Contracts;

namespace BLL.MappingProfiles;

public class ContractMilestoneMapperProfile : Profile
{
    public ContractMilestoneMapperProfile()
    {
        CreateMap<ContractMilestone, ContractMilestoneVM>().ReverseMap();
        CreateMap<ContractMilestone, CreateContractMilestoneVM>().ReverseMap();
        CreateMap<ContractMilestone, UpdateContractMilestoneVM>().ReverseMap();
        CreateMap<ContractMilestone, UpdContractMilestoneStatusFreelancerVM>().ReverseMap();
        
        CreateMap<UpdContractMilestoneStatusEmployerVM, ContractMilestone>()
            .ForMember(dest => dest.Status,
            opt
                => opt.MapFrom(src => (ContractMilestoneStatus)src.Status));
    }
}