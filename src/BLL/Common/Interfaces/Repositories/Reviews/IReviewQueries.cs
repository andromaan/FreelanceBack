using Domain.Models.Reviews;

namespace BLL.Common.Interfaces.Repositories.Reviews;

public interface IReviewQueries : IQueries<Review, Guid>
{
    Task<IEnumerable<Review>> GetByReviewedUser(Guid reviewedId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Review>> GetByReviewerUser(Guid reviewerId, CancellationToken cancellationToken = default);
}