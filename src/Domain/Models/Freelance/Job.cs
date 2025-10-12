using Domain.Common.Abstractions;
using Domain.Models.Auth.Users;

namespace Domain.Models.Freelance;

public class Job : AuditableEntity<Guid>
{
    public required Guid ClientId { get; set; }
    public User? Client { get; set; }

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
