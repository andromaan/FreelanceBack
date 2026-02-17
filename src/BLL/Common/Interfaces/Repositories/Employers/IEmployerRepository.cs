using Domain.Models.Employers;

namespace BLL.Common.Interfaces.Repositories.Employers;

public interface IEmployerRepository : IRepository<Employer, Guid>
{
    public Task<Employer?> CreateAsync(Employer entity, Guid createdBy, CancellationToken token);
}