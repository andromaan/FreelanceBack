using Domain.Models.Auth.Users;

namespace BLL.Common.Interfaces.Repositories.UserProfiles;

public interface IUserProfileRepository : IRepository<UserProfile, Guid>
{
    public Task<UserProfile?> CreateAsync(UserProfile entity, Guid createdBy, CancellationToken token);
}