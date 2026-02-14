using AutoMapper;
using BLL.ViewModels.Portfolio;
using Domain.Models.Freelance;

namespace BLL.MappingProfiles;

public class PortfolioMapperProfile : Profile
{
    public PortfolioMapperProfile()
    {
        CreateMap<Portfolio, PortfolioVM>().ReverseMap();
        CreateMap<Portfolio, CreatePortfolioVM>().ReverseMap();
        CreateMap<Portfolio, UpdatePortfolioVM>().ReverseMap();
    }
}