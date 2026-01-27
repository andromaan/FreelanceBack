using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.ContractMilestones;
using DAL.Data;
using Domain.Models.Freelance;

namespace DAL.Repositories;

public class ContractMilestoneRepository(AppDbContext context, IUserProvider provider)
    : Repository<ContractMilestone, Guid>(context, provider), IContractMilestoneRepository, IContractMilestoneQueries
{
}