using Domain.Models.Auth.Users;

namespace BLL.Common.Interfaces.Repositories.WalletTransactions;

public interface IWalletTransactionRepository : IRepository<WalletTransaction, Guid>
{
}

