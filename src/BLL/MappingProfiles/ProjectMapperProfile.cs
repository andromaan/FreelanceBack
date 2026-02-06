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
        
        CreateMap<CreateProjectVM, Project>()
            .ForMember(dest => dest.Status, 
                opt => opt.MapFrom(src => ProjectStatus.Open));
    }
}