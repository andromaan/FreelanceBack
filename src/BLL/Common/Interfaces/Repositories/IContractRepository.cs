using Domain.Models.Freelance;

namespace BLL.Common.Interfaces.Repositories;

public interface IContractRepository : IRepository<Contract, Guid>, IQueries<Contract, Guid>
{
    
}

