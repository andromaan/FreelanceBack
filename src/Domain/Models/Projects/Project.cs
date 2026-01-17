using Domain.Common.Abstractions;
using Domain.Models.Employers;
using Domain.Models.Freelance;

namespace Domain.Models.Projects;

public class Project : AuditableEntity<Guid>
{
    public required Guid EmployerId { get; set; }
    public Employer? Employer { get; set; }

    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public decimal? BudgetMin { get; set; }
    public decimal? BudgetMax { get; set; }
    public bool IsHourly { get; set; }
    public string Status { get; set; } = nameof(ProjectStatus.Open);

    public ICollection<Proposal> Proposals { get; set; } = new List<Proposal>();
    public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}

public enum ProjectStatus
{
    Open,
    InProgress,
    Completed,
    Cancelled
}
