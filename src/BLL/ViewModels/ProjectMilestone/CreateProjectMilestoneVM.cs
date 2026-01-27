namespace BLL.ViewModels.ProjectMilestone;

public class CreateProjectMilestoneVM
{
    public Guid ProjectId { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
}