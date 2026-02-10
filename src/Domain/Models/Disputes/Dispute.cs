using Domain.Common.Abstractions;

namespace Domain.Models.Disputes;

public class Dispute : AuditableEntity<Guid>
{
    public Guid ContractId { get; set; }
    public string Reason { get; set; } = string.Empty;
    public DisputeStatus Status { get; set; } = DisputeStatus.Open;
}

public enum DisputeStatus
{
    Open,
    UnderReview,
    Resolved,
    Rejected
}