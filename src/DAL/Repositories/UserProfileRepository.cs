using BLL.Common.Interfaces.Repositories;
using DAL.Data;
using DAL.Extensions;
using Domain.Common.Interfaces;
using Domain.Models.Auth.Users;

namespace DAL.Repositories;

public class UserProfileRepository(AppDbContext appDbContext, IUserProvider userProvider)
    : Repository<UserProfile, Guid>(appDbContext, userProvider), IUserProfileRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;
    
    public async Task<UserProfile?> CreateAsync(UserProfile entity, Guid createdBy, CancellationToken token)
    {
        entity.CreatedBy = createdBy;
        await appDbContext.AddAuditableAsync(entity, token);
        await appDbContext.SaveChangesAsync(token);
        return entity;
    }
}