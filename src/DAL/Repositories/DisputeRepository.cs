using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Disputes;
using DAL.Data;
using Domain.Models.Disputes;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class DisputeRepository(AppDbContext context, IUserProvider userProvider)
    : Repository<Dispute, Guid>(context, userProvider), IDisputeRepository, IDisputeQueries
{
    private readonly IUserProvider _userProvider = userProvider;
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Dispute>> GetDisputesByUser(CancellationToken cancellationToken)
    {
        var userId = _userProvider.GetUserId().GetAwaiter().GetResult();
        
        return await _context.Set<Dispute>().Where(p => p.CreatedBy == userId).ToListAsync(cancellationToken);
    }
}