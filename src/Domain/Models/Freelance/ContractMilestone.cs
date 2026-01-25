using Domain.Common.Abstractions;

namespace Domain.Models.Freelance;

public class ContractMilestone : AuditableEntity<Guid>
{
    public Guid ContractId { get; set; }
    public Contract? Contract { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
    public ContractMilestoneStatus Status { get; set; } = ContractMilestoneStatus.Pending;
}

public enum ContractMilestoneStatus
{
    Pending,
    InProgress,
    Submitted,
    UnderReview,
    Approved,
    Rejected
}