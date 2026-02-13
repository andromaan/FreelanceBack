using Domain.Common.Abstractions;

namespace BLL.Common.Interfaces.Repositories;

public interface IByUserQuery<TEntity, TKey> where TEntity : Entity<TKey>
{
    public Task<TEntity?> GetByUser(Guid userId, CancellationToken cancellationToken);
}