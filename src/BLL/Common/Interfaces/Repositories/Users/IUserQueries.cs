using Domain.Models.Auth.Users;

namespace BLL.Common.Interfaces.Repositories.Users;

public interface IUserQueries : IQueries<User, Guid>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken token, bool includes = false);
    Task<bool> IsUniqueEmailAsync(string email, CancellationToken token);
    Task<List<User>> GetUsersByRoleAsync(string roleName, CancellationToken token = default);
    Task<User?> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken);
}