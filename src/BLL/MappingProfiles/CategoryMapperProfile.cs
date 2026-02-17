using AutoMapper;
using BLL.ViewModels.Category;
using Domain.Models.Projects;

namespace BLL.MappingProfiles;

public class CategoryMapperProfile : Profile
{
    public CategoryMapperProfile()
    {
        CreateMap<Category, CategoryVM>().ReverseMap();
        CreateMap<Category, CreateCategoryVM>().ReverseMap();
        CreateMap<Category, UpdateCategoryVM>().ReverseMap();
    }
}