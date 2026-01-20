using AutoMapper;
using BLL.ViewModels.Project;
using Domain.Models.Projects;

namespace BLL.MappingProfiles;

public class ProjectMapperProfile : Profile
{
    public ProjectMapperProfile()
    {
        CreateMap<Project, ProjectVM>().ReverseMap();
        CreateMap<Project, CreateProjectVM>().ReverseMap();
        CreateMap<Project, UpdateProjectVM>().ReverseMap();
    }
}