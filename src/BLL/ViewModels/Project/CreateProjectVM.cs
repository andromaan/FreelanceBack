using BLL.Common.Interfaces;

namespace BLL.ViewModels.Project;

public class CreateProjectVM : ISkipMapper
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public decimal Budget { get; set; }
    public DateTime Deadline { get; set; }
}