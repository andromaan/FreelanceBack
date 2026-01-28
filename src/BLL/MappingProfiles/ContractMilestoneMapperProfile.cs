using AutoMapper;
using BLL.ViewModels.ContractMilestone;
using Domain.Models.Freelance;

namespace BLL.MappingProfiles;

public class ContractMilestoneMapperProfile : Profile
{
    public ContractMilestoneMapperProfile()
    {
        CreateMap<ContractMilestone, ContractMilestoneVM>().ReverseMap();
        CreateMap<ContractMilestone, CreateContractMilestoneVM>().ReverseMap();
        CreateMap<ContractMilestone, UpdateContractMilestoneVM>().ReverseMap();
    }
}