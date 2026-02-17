using Domain.Common.Abstractions;

namespace BLL.Common.Interfaces.Repositories;

public interface IRepository<TEntity, in TKey>
    where TEntity : Entity<TKey>
{
    Task<TEntity?> CreateAsync(TEntity entity, CancellationToken token);
    Task<TEntity?> UpdateAsync(TEntity entity, CancellationToken token);
    Task<TEntity?> DeleteAsync(TKey id, CancellationToken token);
}