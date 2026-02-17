using Domain.Models.Contracts;

namespace BLL.Common.Interfaces.Repositories.Contracts;

public interface IContractQueries : IQueries<Contract, Guid>
{
    Task<IEnumerable<Contract>> GetByUser(CancellationToken cancellationToken);
    Task<IEnumerable<Contract>> GetByFreelancerId(Guid freelancerId, CancellationToken cancellationToken);
}