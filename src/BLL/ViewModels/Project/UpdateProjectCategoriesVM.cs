using BLL.Common.Interfaces;

namespace BLL.ViewModels.Project;

public class UpdateProjectCategoriesVM : ISkipMapper
{
    public List<int> CategoryIds { get; set; } = [];
}