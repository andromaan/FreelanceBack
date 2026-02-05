using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.ContractMilestones;
using DAL.Data;
using Domain.Models.Freelance;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class ContractMilestoneRepository(AppDbContext context, IUserProvider provider)
    : Repository<ContractMilestone, Guid>(context, provider), IContractMilestoneRepository, IContractMilestoneQueries
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<ContractMilestone>> GetByContractIdAsync(Guid contractId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<ContractMilestone>().Where(x => x.ContractId == contractId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}