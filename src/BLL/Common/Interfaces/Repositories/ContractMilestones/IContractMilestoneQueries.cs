using Domain.Models.Contracts;

namespace BLL.Common.Interfaces.Repositories.ContractMilestones;

public interface IContractMilestoneQueries : IQueries<ContractMilestone, Guid>
{
    Task<IEnumerable<ContractMilestone>> GetByContractIdAsync(Guid contractId, CancellationToken cancellationToken = default);
}