using Domain.Models.Payments;

namespace BLL.Common.Interfaces.Repositories.WalletTransactions;

public interface IWalletTransactionRepository : IRepository<WalletTransaction, Guid>
{
}

