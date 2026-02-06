using Domain.Models.Auth;

namespace BLL.Common.Interfaces.Repositories.RefreshTokens;

public interface IRefreshTokenRepository : IRepository<RefreshToken, string>, IQueries<RefreshToken, string>
{
    Task<RefreshToken?> GetRefreshTokenAsync(string refreshToken, CancellationToken token);
    Task MakeAllRefreshTokensExpiredForUser(Guid userId, CancellationToken token);
}