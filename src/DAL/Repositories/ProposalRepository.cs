using BLL.Common.Interfaces.Repositories;
using DAL.Data;
using Domain.Common.Interfaces;
using Domain.Models.Freelance;

namespace DAL.Repositories;

public class ProposalRepository(AppDbContext appDbContext, IUserProvider userProvider)
    : Repository<Proposal, Guid>(appDbContext, userProvider), IProposalRepository
{
    
}