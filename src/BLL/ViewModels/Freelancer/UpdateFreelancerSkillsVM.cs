using BLL.Common.Interfaces;

namespace BLL.ViewModels.Freelancer;

public class UpdateFreelancerSkillsVM : ISkipMapper
{
    public List<int> SkillIds { get; init; } = [];
}