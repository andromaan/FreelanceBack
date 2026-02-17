using Domain.Models.Reviews;

namespace BLL.Common.Interfaces.Repositories.Reviews;

public interface IReviewQueries : IQueries<Review, Guid>
{
    Task<IEnumerable<Review>> GetReviewsByReviewedUser(Guid reviewedUserId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Review>> GetReviewsByReviewerUser(Guid reviewerId, CancellationToken cancellationToken = default);
    Task<Review?> GetByReviewerAndReviewedUser(Guid reviewerId, Guid reviewedUserId, CancellationToken cancellationToken = default);
}