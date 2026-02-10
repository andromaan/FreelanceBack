using Domain.Models.Disputes;

namespace BLL.Common.Interfaces.Repositories.Disputes;

public interface IDisputeQueries : IQueries<Dispute, Guid>
{
    Task<IEnumerable<Dispute>> GetDisputesByUser(CancellationToken cancellationToken);
}