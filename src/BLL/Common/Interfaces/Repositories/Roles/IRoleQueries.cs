using Domain.Models.Auth;

namespace BLL.Common.Interfaces.Repositories.Roles;

public interface IRoleQueries : IQueries<Role, int>
{
    Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}