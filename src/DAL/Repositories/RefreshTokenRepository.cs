using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.RefreshTokens;
using DAL.Data;
using Domain.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class RefreshTokenRepository(AppDbContext appDbContext, IUserProvider userProvider)
    : Repository<RefreshToken, string>(appDbContext, userProvider), IRefreshTokenRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<RefreshToken?> GetRefreshTokenAsync(string refreshToken, CancellationToken token)
    {
        var entity = await _appDbContext.RefreshTokens
            .FirstOrDefaultAsync(t => t.Token == refreshToken, token);

        return entity;
    }

    public async Task MakeAllRefreshTokensExpiredForUser(Guid userId, CancellationToken token)
    {
        // ExecuteUpdateAsync не працює з InMemory провайдером
        // Використовуємо універсальний підхід
        var tokensToExpire = await _appDbContext.RefreshTokens
            .Where(t => t.UserId == userId && !t.IsUsed)
            .ToListAsync(token);

        foreach (var refreshToken in tokensToExpire)
        {
            refreshToken.IsUsed = true;
        }

        if (tokensToExpire.Any())
        {
            await _appDbContext.SaveChangesAsync(token);
        }
    }
}