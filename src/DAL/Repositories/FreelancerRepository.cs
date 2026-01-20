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
    
    public async Task<Freelancer?> CreateAsync(Freelancer entity, Guid createdBy, CancellationToken token)
    {
        entity.CreatedBy = createdBy;
        await _appDbContext.AddAuditableAsync(entity, token);
        await _appDbContext.SaveChangesAsync(token);
        return entity;
    }

    public async Task<Freelancer?> GetByUserId(Guid userId, CancellationToken token, bool includes = false)
    {
        var query = _appDbContext.Freelancers.AsQueryable();

        if (includes)
        {
            query = query
                .Include(up => up.Skills)
                .Include(up => up.Portfolio)
                .Include(up => up.Country)
                .Include(up => up.Languages);
        }
        
        return await query.FirstOrDefaultAsync(up => up.UserId == userId, token);
    }
}