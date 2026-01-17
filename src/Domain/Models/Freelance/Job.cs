using Domain.Common.Abstractions;
using Domain.Models.Auth.Users;
using Domain.Models.Employers;

namespace Domain.Models.Freelance;

public class Job : AuditableEntity<Guid>
{
    public required Guid EmployerId { get; set; }
    public Employer? Client { get; set; }

    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public decimal? BudgetMin { get; set; }
    public decimal? BudgetMax { get; set; }
    public bool IsHourly { get; set; }
    public string Status { get; set; } = nameof(JobStatus.Open);

    public ICollection<Proposal> Proposals { get; set; } = new List<Proposal>();
    public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}

public enum JobStatus
{
    Open,
    InProgress,
    Completed,
    Cancelled
}
