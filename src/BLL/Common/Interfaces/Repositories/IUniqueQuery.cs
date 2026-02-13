using Domain.Common.Abstractions;

namespace BLL.Common.Interfaces.Repositories;

public interface IUniqueQuery<in TEntity, TKey> where TEntity : Entity<TKey>
{
    Task<bool> IsUniqueAsync(TEntity entity, CancellationToken token);
}