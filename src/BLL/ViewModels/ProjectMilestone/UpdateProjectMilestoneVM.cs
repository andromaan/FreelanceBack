using BLL.Common.Interfaces;
using Domain.Models.Projects;

namespace BLL.ViewModels.ProjectMilestone;

public class UpdateProjectMilestoneVM : ISkipMapper
{
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
    public ProjectMilestoneStatus Status { get; set; }
}