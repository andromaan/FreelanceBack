using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Proposals;
using DAL.Data;
using Domain.Models.Freelance;

namespace DAL.Repositories;

public class ProposalRepository(AppDbContext appDbContext, IUserProvider userProvider)
    : Repository<Proposal, Guid>(appDbContext, userProvider), IProposalRepository, IProposalQueries
{
    
}