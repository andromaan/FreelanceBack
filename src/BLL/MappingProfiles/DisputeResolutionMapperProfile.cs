using AutoMapper;
using BLL.ViewModels.DisputeResolution;
using Domain.Models.Disputes;

namespace BLL.MappingProfiles;

public class DisputeResolutionMapperProfile : Profile
{
    public DisputeResolutionMapperProfile()
    {
        CreateMap<DisputeResolution, DisputeResolutionVM>()
            .ForMember(dest => dest.ResolvedById, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest => dest.ResolutionDate, opt => opt.MapFrom(src => src.CreatedAt));
        CreateMap<CreateDisputeResolutionVM, DisputeResolution>().ForSourceMember(src => src.DisputeStatus, opt => opt.DoNotValidate());
    }
}