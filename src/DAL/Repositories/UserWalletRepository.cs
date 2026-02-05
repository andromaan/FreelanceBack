using BLL.Common.Interfaces.Repositories.UserWallets;
using DAL.Data;
using DAL.Extensions;
using Domain.Models.Auth.Users;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class UserWalletRepository(AppDbContext appDbContext)
    : IUserWalletRepository, IUserWalletQueries
{
    public async Task<UserWallet?> CreateAsync(UserWallet balance, CancellationToken token)
    {
        await appDbContext.AddAuditableAsync(balance, token);

        return balance;
    }

    public async Task<UserWallet?> DepositAsync(Guid userId, decimal amount, CancellationToken token)
    {
        var balance = await appDbContext.Set<UserWallet>()
            .FirstOrDefaultAsync(b => b.CreatedBy == userId, token);

        balance!.Balance += amount;

        await appDbContext.SaveChangesAsync(token);

        return balance;
    }

    public async Task<bool> IsWithdrawSuccessAsync(Guid userId, decimal amount, CancellationToken token)
    {
        var balance = await appDbContext.Set<UserWallet>().AsNoTracking()
            .FirstOrDefaultAsync(b => b.CreatedBy == userId, token);

        if (balance!.Balance < amount)
        {
            return false;
        }

        return true;
    }

    public async Task<UserWallet?> WithdrawAsync(Guid userId, decimal amount, CancellationToken token)
    {
        var balance = await appDbContext.Set<UserWallet>()
            .FirstOrDefaultAsync(b => b.CreatedBy == userId, token);

        if (balance!.Balance < amount)
        {
            return null;
        }

        balance.Balance -= amount;

        await appDbContext.SaveChangesAsync(token);

        return balance;
    }

    public async Task<decimal?> GetAmountByUserAsync(Guid userId, CancellationToken token)
    {
        return (await appDbContext.Set<UserWallet>()
            .FirstOrDefaultAsync(b => b.CreatedBy == userId, token))!.Balance;
    }
}