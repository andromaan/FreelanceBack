using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.WalletTransactions;
using DAL.Data;
using Domain.Models.Auth.Users;

namespace DAL.Repositories;

public class WalletTransactionRepository(AppDbContext context, IUserProvider userProvider)
    : Repository<WalletTransaction, Guid>(context, userProvider), IWalletTransactionRepository, IWalletTransactionQueries
{
}