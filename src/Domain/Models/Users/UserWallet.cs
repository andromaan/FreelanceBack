using Domain.Common.Abstractions;

namespace Domain.Models.Users;

public class UserWallet : AuditableEntity<Guid>
{
    public decimal Balance { get; set; }
    public string Currency { get; set; } = string.Empty;
}