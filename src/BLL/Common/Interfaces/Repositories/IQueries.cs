using Domain.Common.Abstractions;

namespace BLL.Common.Interfaces.Repositories;

public interface IQueries<TEntity, in TKey>
    where TEntity : Entity<TKey>
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken token);
    Task<TEntity?> GetByIdAsync(TKey id, CancellationToken token, bool asNoTracking = false);
    Task<(List<TEntity> Entities, int TotalCount)> GetPaginatedAsync(int page, int pageSize, CancellationToken token = default);
}