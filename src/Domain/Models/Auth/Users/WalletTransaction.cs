using Domain.Common.Abstractions;

namespace Domain.Models.Auth.Users;

public class WalletTransaction : Entity<Guid>
{
    public Guid WalletId { get; set; }
    public UserWallet? Wallet { get; set; }
    public decimal Amount { get; set; }
    public string? TransactionType { get; set; } // TODO enum
    public DateTime TransactionDate { get; set; }
}