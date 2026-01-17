using AutoMapper;
using Domain.Models.Projects;
using Domain.ViewModels.Category;

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