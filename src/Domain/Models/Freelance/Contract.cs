using Domain.Common.Abstractions;
using Domain.Models.Auth.Users;

namespace Domain.Models.Freelance;

public class Contract : AuditableEntity<Guid>
{
    public required Guid JobId { get; set; }
    public Job? Job { get; set; }

    public required Guid ClientId { get; set; }
    public User? Client { get; set; }

    public required Guid FreelancerId { get; set; }
    public User? Freelancer { get; set; }

    public decimal Amount { get; set; }
    public string Status { get; set; } = nameof(ContractStatus.Draft);
}

public enum ContractStatus
{
    Draft,
    Active,
    Completed,
    Cancelled
}
