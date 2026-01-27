using AutoMapper;
using BLL.ViewModels.Quote;
using Domain.Models.Projects;

namespace BLL.MappingProfiles;

public class QuoteMapperProfile : Profile
{
    public QuoteMapperProfile()
    {
        CreateMap<Quote, QuoteVM>().ReverseMap();
        CreateMap<Quote, CreateQuoteVM>().ReverseMap();
        CreateMap<Quote, UpdateQuoteVM>().ReverseMap();
    }
}