using Domain.Models.Payments;

namespace BLL.Common.Interfaces.Repositories.WalletTransactions;

public interface IWalletTransactionQueries : IQueries<WalletTransaction, Guid>
{
}