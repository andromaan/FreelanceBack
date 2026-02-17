using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Freelancers;
using DAL.Data;
using DAL.Extensions;
using Domain.Models.Freelance;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class FreelancerRepository(AppDbContext appDbContext, IUserProvider userProvider)
    : Repository<Freelancer, Guid>(appDbContext, userProvider), IFreelancerRepository, IFreelancerQueries
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public override Task<Freelancer?> GetByIdAsync(Guid id, CancellationToken token, bool asNoTracking = false)
    {
        return base.GetByIdAsync(id, token, asNoTracking,
            freelancer => freelancer.Portfolio,
            freelancer => freelancer.Skills);
    }

    public async Task<Freelancer?> CreateAsync(Freelancer entity, Guid createdBy, CancellationToken token)
    {
        entity.CreatedBy = createdBy;
        await _appDbContext.AddAuditableAsync(entity, token);
        await _appDbContext.SaveChangesAsync(token);
        return entity;
    }

    public async Task<Freelancer?> GetByUserIdAsync(Guid userId, CancellationToken token, bool includes = false)
    {
        var query = _appDbContext.Freelancers.AsQueryable();

        if (includes)
        {
            query = query
                .Include(up => up.Skills)
                .Include(up => up.Portfolio);
        }

        return await query.FirstOrDefaultAsync(up => up.CreatedBy == userId, token);
    }

    public async Task<Freelancer?> GetByUser(Guid userId, CancellationToken cancellationToken)
    {
        return await _appDbContext.Freelancers
            .Include(up => up.Skills)
            .Include(up => up.Portfolio)
            .FirstOrDefaultAsync(up => up.CreatedBy == userId, cancellationToken);
    }
}