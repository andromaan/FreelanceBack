namespace BLL.Common.Interfaces.Repositories.UserWallets;

public interface IUserWalletQueries
{
    Task<decimal?> GetAmountByUserAsync(Guid userId, CancellationToken token);
    Task<bool> IsWithdrawSuccessAsync(Guid userId, decimal amount, CancellationToken token);
}