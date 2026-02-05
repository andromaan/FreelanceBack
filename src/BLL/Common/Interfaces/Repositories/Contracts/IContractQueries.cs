using Domain.Models.Freelance;

namespace BLL.Common.Interfaces.Repositories.Contracts;

public interface IContractQueries : IQueries<Contract, Guid>
{
    Task<IEnumerable<Contract>> GetByUser(CancellationToken cancellationToken);
}