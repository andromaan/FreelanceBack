using Domain.Models.Freelance;

namespace BLL.Common.Interfaces.Repositories;

public interface IProposalRepository : IRepository<Proposal, Guid>, IQueries<Proposal, Guid>
{
    
}