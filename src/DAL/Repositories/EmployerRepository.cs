using BLL.Common.Interfaces.Repositories.Employers;
using DAL.Data;
using DAL.Extensions;
using Domain.Common.Interfaces;
using Domain.Models.Employers;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class EmployerRepository(AppDbContext appDbContext, IUserProvider userProvider)
    : Repository<Employer, Guid>(appDbContext, userProvider), IEmployerRepository, IEmployerQueries
{
    private readonly AppDbContext _appDbContext = appDbContext;
    
    public async Task<Employer?> CreateAsync(Employer entity, Guid createdBy, CancellationToken token)
    {
        entity.CreatedBy = createdBy;
        await _appDbContext.AddAuditableAsync(entity, token);
        await _appDbContext.SaveChangesAsync(token);
        return entity;
    }

    public async Task<Employer?> GetByUserId(Guid userId, CancellationToken token, bool includes = false)
    {
        var query = _appDbContext.Employers.AsQueryable();

        if (includes)
        {
            query = query;
        }
        
        return await query.FirstOrDefaultAsync(up => up.UserId == userId, token);
    }
}