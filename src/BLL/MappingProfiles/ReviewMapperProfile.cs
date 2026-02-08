using AutoMapper;
using BLL.ViewModels.Reviews;
using Domain.Models.Reviews;

namespace BLL.MappingProfiles;

public class ReviewMapperProfile : Profile
{
    public ReviewMapperProfile()
    {
        CreateMap<Review, ReviewVM>().ForMember(dest => dest.ReviewerId,
            opt => opt.MapFrom(src => src.CreatedBy));
        CreateMap<Review, CreateReviewVM>().ReverseMap();
        CreateMap<Review, UpdateReviewVM>().ReverseMap();
    }
}