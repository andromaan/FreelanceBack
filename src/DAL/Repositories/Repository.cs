using System.Linq.Expressions;
using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories;
using DAL.Data;
using DAL.Extensions;
using Domain.Common.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class Repository<TEntity, TKey>(AppDbContext appDbContext, IUserProvider userProvider)
    : IRepository<TEntity, TKey>, IQueries<TEntity, TKey>
    where TEntity : Entity<TKey>
{
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken token) =>
        await appDbContext.Set<TEntity>().AsNoTracking().ToListAsync(token);

    protected async Task<IEnumerable<TEntity>> GetAllAsync(
        CancellationToken token,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = appDbContext.Set<TEntity>().AsNoTracking();

        foreach (var include in includes)
            query = query.Include(include);

        return await query.ToListAsync(token);
    }

    public virtual async Task<TEntity?> CreateAsync(TEntity entity, CancellationToken token)
    {
        if (entity is AuditableEntity<TKey> auditable)
        {
            auditable.CreatedBy =
                auditable.CreatedBy == Guid.Empty ? await userProvider.GetUserId() : auditable.CreatedBy;

            await appDbContext.AddAuditableAsync(auditable, token);
        }
        else
        {
            await appDbContext.Set<TEntity>().AddAsync(entity, token);
        }

        await SaveAsync(token);
        return entity;
    }

    public virtual async Task<TEntity?> UpdateAsync(TEntity entity, CancellationToken token)
    {
        if (entity is AuditableEntity<TKey> auditable)
        {
            auditable.ModifiedBy = await userProvider.GetUserId();

            appDbContext.UpdateAuditable(auditable);
        }
        else
        {
            appDbContext.Set<TEntity>().Update(entity);
        }

        await SaveAsync(token);
        return entity;
    }

    public virtual async Task<TEntity?> DeleteAsync(TKey id, CancellationToken token)
    {
        var entity = await appDbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id!.Equals(id), token);
        appDbContext.Set<TEntity>().Remove(entity!);
        await SaveAsync(token);
        return entity;
    }

    public virtual async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken token, bool asNoTracking = false)
    {
        var query = appDbContext.Set<TEntity>().AsQueryable();

        if (asNoTracking)
            query = query.AsNoTracking();
        return await query.FirstOrDefaultAsync(e => e.Id!.Equals(id), token);
    }

    public async Task<(List<TEntity> Entities, int TotalCount)> GetPaginatedAsync(int page, int pageSize,
        CancellationToken token = default)
    {
        var query = appDbContext.Set<TEntity>()
            .AsQueryable();

        var totalCount = await query.CountAsync(token);

        var entities = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(token);

        return (entities, totalCount);
    }

    protected async Task<TEntity?> GetByIdAsync(
        TKey id,
        CancellationToken token,
        bool asNoTracking = false,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = appDbContext.Set<TEntity>();

        if (asNoTracking)
            query = query.AsNoTracking();

        foreach (var include in includes)
            query = query.Include(include);

        return await query.FirstOrDefaultAsync(e => e.Id!.Equals(id), token);
    }

    private async Task SaveAsync(CancellationToken token) => await appDbContext.SaveChangesAsync(token);
}