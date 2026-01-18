using Domain.Common.Abstractions;

namespace BLL.Common;

public interface IUniqueQuery<TEntity, TKey> where TEntity : Entity<TKey>
{
    Task<bool> IsUniqueAsync(TEntity entity, CancellationToken token);
}