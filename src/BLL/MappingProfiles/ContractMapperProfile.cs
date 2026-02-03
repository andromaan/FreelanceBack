using AutoMapper;
using BLL.ViewModels.Contract;
using Domain.Models.Freelance;

namespace BLL.MappingProfiles;

public class ContractMapperProfile : Profile
{
    public ContractMapperProfile()
    {
        CreateMap<Contract, ContractVM>().ReverseMap();
        CreateMap<Contract, UpdateContractVM>().ReverseMap();
        CreateMap<Contract, UpdateContractStatusVM>().ReverseMap();
    }
}