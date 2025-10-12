using Domain.Common.Abstractions;
using Domain.Models.Freelance;

namespace Domain.Models.Payments;

public class Payment : AuditableEntity<Guid>
{
    public required Guid ContractId { get; set; }
    public Contract? Contract { get; set; }

    public string? StripePaymentIntentId { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; } = nameof(PaymentStatus.Pending);
}

public enum PaymentStatus
{
    Pending,
    Succeeded,
    Failed
}
