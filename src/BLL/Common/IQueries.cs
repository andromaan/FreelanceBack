using Domain.Common.Abstractions;

namespace BLL.Common;

public interface IQueries<TEntity, in TKey>
    where TEntity : Entity<TKey>
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken token);
    Task<TEntity?> GetByIdAsync(TKey id, CancellationToken token, bool asNoTracking = false);
}