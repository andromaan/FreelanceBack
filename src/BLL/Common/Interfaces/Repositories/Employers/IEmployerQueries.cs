using Domain.Models.Employers;

namespace BLL.Common.Interfaces.Repositories.Employers;

public interface IEmployerQueries : IQueries<Employer, Guid>, IByUserQuery<Employer, Guid>
{
    Task<Employer?> GetByUserId(Guid userId, CancellationToken token);
}