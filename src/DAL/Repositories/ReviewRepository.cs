using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Reviews;
using DAL.Data;
using Domain.Models.Reviews;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class ReviewRepository(AppDbContext context, IUserProvider userProvider)
    : Repository<Review, Guid>(context, userProvider), IReviewRepository, IReviewQueries
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Review>> GetByReviewedUser(Guid reviewedId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Review>().Where(r => r.ReviewedUserId == reviewedId).ToListAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<Review>> GetByReviewerUser(Guid reviewerId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Review>().Where(r => r.CreatedBy == reviewerId).ToListAsync(cancellationToken);
    }
}