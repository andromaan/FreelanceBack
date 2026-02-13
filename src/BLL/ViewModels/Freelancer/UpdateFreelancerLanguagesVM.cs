using BLL.Common.Interfaces;

namespace BLL.ViewModels.Freelancer;

public class UpdateFreelancerLanguagesVM : ISkipMapper
{
    public List<int> LanguageIds { get; init; } = [];
}