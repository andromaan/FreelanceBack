using BLL.Common.Interfaces.Repositories;
using DAL.Data;
using Domain.Common.Interfaces;
using Domain.Models.Freelance;

namespace DAL.Repositories;

public class ContractRepository(AppDbContext appDbContext, IUserProvider userProvider)
    : Repository<Contract, Guid>(appDbContext, userProvider), IContractRepository
{
    
}

