using AutoMapper;
using BLL.ViewModels.Dispute;
using Domain.Models.Disputes;

namespace BLL.MappingProfiles;

public class DisputeMapperProfile : Profile
{
    public DisputeMapperProfile()
    {
        CreateMap<Dispute, DisputeVM>().ReverseMap();
        CreateMap<Dispute, CreateDisputeVM>().ReverseMap();
        
        CreateMap<CreateDisputeVM, Dispute>()
            .ForMember(dest => dest.Status, 
                opt => opt.MapFrom(src => DisputeStatus.Open));
    }
}