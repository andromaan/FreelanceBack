using System.Linq.Expressions;
using BLL.Common.Interfaces.Repositories;
using DAL.Data;
using Domain.Common.Interfaces;
using Domain.Models.Auth.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class UserRepository(AppDbContext appDbContext, IUserProvider userProvider)
    : Repository<User, Guid>(appDbContext, userProvider), IUserRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<User?> GetByEmailAsync(string email, CancellationToken token, bool includes = false)
    {
        return await GetUserAsync(u => u.Email == email, token, includes);
    }

    public override async Task<User?> GetByIdAsync(Guid id, CancellationToken token, bool includes = false)
    {
        return await GetUserAsync(u => u.Id == id, token, includes);
    }

    private async Task<User?> GetUserAsync(Expression<Func<User, bool>> predicate, CancellationToken token,
        bool includes = false)
    {
        if (includes)
        {
            return await _appDbContext.Users
                .Include(ur => ur.Role)
                .FirstOrDefaultAsync(predicate, token);
        }

        return await _appDbContext.Users
            .FirstOrDefaultAsync(predicate, token);
    }

    public async Task<List<User>> GetUsersByRoleAsync(string roleName, CancellationToken token = default)
    {
        return await _appDbContext.Users
            .Include(u => u.Role)
            .Where(u => u.Role != null && u.Role.Name == roleName)
            .ToListAsync(token);
    }

    public async Task<User?> FindByLoginAsync(string loginProvider, string providerKey,
        CancellationToken cancellationToken)
    {
        var user = await _appDbContext.Users
            .FirstOrDefaultAsync(
                u => u.ExternalProvider == loginProvider &&
                     u.ExternalProviderKey == providerKey,
                cancellationToken);

        return user;
    }
    
    public async Task<IdentityResult> AddLoginAsync(User user, UserLoginInfo loginInfo,
        CancellationToken cancellationToken)
    {
        var userEntity = await GetUserAsync(x => x.Id == user.Id, cancellationToken);

        if (userEntity == null)
        {
            return IdentityResult.Failed(new IdentityError { Code = "NotFound", Description = "User not found." });
        }

        userEntity.ExternalProvider = loginInfo.LoginProvider;
        userEntity.ExternalProviderKey = loginInfo.ProviderKey;

        await _appDbContext.SaveChangesAsync(cancellationToken);
        return IdentityResult.Success;
    }

    public async Task<bool> IsUniqueEmailAsync(string email, CancellationToken token)
    {
        return await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == email, token) == null;
    }
}