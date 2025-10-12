using Domain.Common.Abstractions;
using Domain.Models.Auth.Users;

namespace Domain.Models.Freelance;

public class Proposal : AuditableEntity<Guid>
{
    public required Guid JobId { get; set; }
    public Job? Job { get; set; }

    public required Guid FreelancerId { get; set; }
    public User? Freelancer { get; set; }

    public decimal Price { get; set; }
    public string? CoverLetter { get; set; }
    public string Status { get; set; } = nameof(ProposalStatus.Pending);
}

public enum ProposalStatus
{
    Pending,
    Accepted,
    Rejected
}
