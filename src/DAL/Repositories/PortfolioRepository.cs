using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Portfolios;
using DAL.Data;
using Domain.Models.Freelance;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class PortfolioRepository(AppDbContext appDbContext, IUserProvider userProvider)
    : Repository<Portfolio, Guid>(appDbContext, userProvider), IPortfolioRepository, IPortfolioQueries
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<List<Portfolio>> GetByFreelancerIdAsync(Guid requestFreelancerId,
        CancellationToken cancellationToken)
    {
        return await _appDbContext.Set<Portfolio>().Where(p => p.FreelancerId == requestFreelancerId)
            .ToListAsync(cancellationToken);
    }
}