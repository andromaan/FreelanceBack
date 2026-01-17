using Domain.Models.Employers;

namespace BLL.Common.Interfaces.Repositories.Employers;

public interface IEmployerQueries : IQueries<Employer, Guid>
{
    Task<Employer?> GetByUserId(Guid userId, CancellationToken token, bool includes = false);
}