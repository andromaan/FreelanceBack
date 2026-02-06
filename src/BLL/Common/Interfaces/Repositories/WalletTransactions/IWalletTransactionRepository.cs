using Domain.Models.Users;

namespace BLL.Common.Interfaces.Repositories.WalletTransactions;

public interface IWalletTransactionRepository : IRepository<WalletTransaction, Guid>
{
}

