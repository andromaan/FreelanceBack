using Domain.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace BLL.Common.Interfaces.Repositories.Users;

public interface IUserRepository : IRepository<User, Guid>
{
    Task<IdentityResult> AddLoginAsync(User user, UserLoginInfo loginInfo, CancellationToken cancellationToken);
}