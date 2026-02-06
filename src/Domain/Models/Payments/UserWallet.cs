using Domain.Common.Abstractions;

namespace Domain.Models.Payments;

public class UserWallet : AuditableEntity<Guid>
{
    public decimal Balance { get; set; }
    public string Currency { get; set; } = string.Empty;
}