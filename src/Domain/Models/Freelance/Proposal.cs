using Domain.Common.Abstractions;
using Domain.Models.Projects;

namespace Domain.Models.Freelance;

public class Proposal : AuditableEntity<Guid>
{
    public required Guid ProjectId { get; set; }
    public Project? Project { get; set; }

    public required Guid FreelancerId { get; set; }
    public Freelancer? Freelancer { get; set; }

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
