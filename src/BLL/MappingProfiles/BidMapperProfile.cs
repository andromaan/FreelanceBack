using AutoMapper;
using BLL.ViewModels.Bid;
using Domain.Models.Projects;

namespace BLL.MappingProfiles;

public class BidMapperProfile : Profile
{
    public BidMapperProfile()
    {
        CreateMap<Bid, BidVM>().ReverseMap();
        CreateMap<Bid, CreateBidVM>().ReverseMap();
        CreateMap<Bid, UpdateBidVM>().ReverseMap();
        
        CreateMap<CreateBidVM, Bid>()
            .ForMember(dest => dest.FreelancerId, 
                opt => opt.MapFrom(src => ProjectMilestoneStatus.Pending));
    }
}