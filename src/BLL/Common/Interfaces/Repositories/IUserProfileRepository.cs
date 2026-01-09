using Domain.Models.Auth.Users;

namespace BLL.Common.Interfaces.Repositories;

public interface IUserProfileRepository : IRepository<UserProfile, Guid>, IQueries<UserProfile, Guid>
{
    public Task<UserProfile?> CreateAsync(UserProfile entity, Guid createdBy, CancellationToken token);
}