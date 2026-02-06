using Domain.Models.Users;

namespace BLL.Common.Interfaces.Repositories.UserWallets;

public interface IUserWalletRepository
{
    Task<UserWallet?> CreateAsync(UserWallet userWallet, CancellationToken token);
    Task<UserWallet?> DepositAsync(Guid userId, decimal amount, CancellationToken token);
    Task<UserWallet?> WithdrawAsync(Guid userId, decimal amount, CancellationToken token);
    
}

