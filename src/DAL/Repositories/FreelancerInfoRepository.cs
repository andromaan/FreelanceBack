using BLL.Common.Interfaces.Repositories.FreelancersInfo;
using DAL.Data;
using DAL.Extensions;
using Domain.Common.Interfaces;
using Domain.Models.Freelance;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class FreelancerInfoRepository(AppDbContext appDbContext, IUserProvider userProvider)
    : Repository<FreelancerInfo, Guid>(appDbContext, userProvider), IFreelancerInfoRepository, IFreelancerInfoQueries
{
    private readonly AppDbContext _appDbContext = appDbContext;
    
    public async Task<FreelancerInfo?> CreateAsync(FreelancerInfo entity, Guid createdBy, CancellationToken token)
    {
        entity.CreatedBy = createdBy;
        await _appDbContext.AddAuditableAsync(entity, token);
        await _appDbContext.SaveChangesAsync(token);
        return entity;
    }

    public async Task<FreelancerInfo?> GetByUserId(Guid userId, CancellationToken token, bool includes = false)
    {
        var query = _appDbContext.FreelancersInfo.AsQueryable();

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