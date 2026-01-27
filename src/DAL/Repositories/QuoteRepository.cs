using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Quotes;
using DAL.Data;
using Domain.Models.Projects;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class QuoteRepository(AppDbContext context, IUserProvider provider)
    : Repository<Quote, Guid>(context, provider), IQuoteRepository, IQuoteQueries
{
    private readonly AppDbContext _context = context;
    
    public async Task<IEnumerable<Quote>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Quote>().Where(x => x.ProjectId == projectId)
            .ToListAsync(cancellationToken);
    }
}