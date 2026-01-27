using Domain.Models.Projects;

namespace BLL.Common.Interfaces.Repositories.Bids;

public interface IBidQueries : IQueries<Bid, Guid>
{
    Task<IEnumerable<Bid>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default);
    // Task<IEnumerable<Bid>> GetByFreelancerIdAsync(Guid freelancerId, CancellationToken cancellationToken = default);
}