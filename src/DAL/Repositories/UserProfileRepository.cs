using BLL.Common.Interfaces.Repositories.UserProfiles;
using DAL.Data;
using DAL.Extensions;
using Domain.Common.Interfaces;
using Domain.Models.Auth.Users;

namespace DAL.Repositories;

public class UserProfileRepository(AppDbContext appDbContext, IUserProvider userProvider)
    : Repository<UserProfile, Guid>(appDbContext, userProvider), IUserProfileRepository, IUserProfileQueries
{
    private readonly AppDbContext _appDbContext = appDbContext;
    
    public async Task<UserProfile?> CreateAsync(UserProfile entity, Guid createdBy, CancellationToken token)
    {
        entity.CreatedBy = createdBy;
        await _appDbContext.AddAuditableAsync(entity, token);
        await _appDbContext.SaveChangesAsync(token);
        return entity;
    }
}