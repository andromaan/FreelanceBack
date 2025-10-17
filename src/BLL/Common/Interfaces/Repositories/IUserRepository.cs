using Domain.Models.Auth.Users;
using Microsoft.AspNetCore.Identity;

namespace BLL.Common.Interfaces.Repositories;

public interface IUserRepository : IRepository<User, Guid>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken token, bool includes = false);
    Task<bool> IsUniqueEmailAsync(string email, CancellationToken token);
    Task<List<User>> GetUsersByRoleAsync(string roleName, CancellationToken token = default);
    Task<User?> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken);
    Task<IdentityResult> AddLoginAsync(User user, UserLoginInfo loginInfo, CancellationToken cancellationToken);
}