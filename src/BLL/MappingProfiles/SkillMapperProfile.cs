using AutoMapper;
using BLL.ViewModels.Skill;
using Domain.Models.Projects;

namespace BLL.MappingProfiles;

public class SkillMapperProfile : Profile
{
    public SkillMapperProfile()
    {
        CreateMap<Skill, SkillVM>().ReverseMap();
        CreateMap<Skill, CreateSkillVM>().ReverseMap();
        CreateMap<Skill, UpdateSkillVM>().ReverseMap();
    }
}