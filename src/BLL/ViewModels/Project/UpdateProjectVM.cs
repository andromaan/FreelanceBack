namespace BLL.ViewModels.Project;

public class UpdateProjectVM
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public decimal? BudgetMin { get; set; }
    public decimal? BudgetMax { get; set; }
    public bool IsHourly { get; set; }
}