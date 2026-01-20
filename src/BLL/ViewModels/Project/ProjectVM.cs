using BLL.ViewModels.Category;

namespace BLL.ViewModels.Project;

public class ProjectVM
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public decimal? BudgetMin { get; set; }
    public decimal? BudgetMax { get; set; }
    public bool IsHourly { get; set; }
    public string Status { get; set; } = string.Empty;
    // public ICollection<Proposal> Proposals { get; set; } = new List<Proposal>();
    // public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    public List<CategoryVM> Categories { get; set; } = new();
}