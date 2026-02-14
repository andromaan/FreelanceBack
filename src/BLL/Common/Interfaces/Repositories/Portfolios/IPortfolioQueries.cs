using Domain.Models.Freelance;

namespace BLL.Common.Interfaces.Repositories.Portfolios;

public interface IPortfolioQueries : IQueries<Portfolio, Guid>
{
    Task<List<Portfolio>> GetByFreelancerIdAsync(Guid requestFreelancerId, CancellationToken cancellationToken);
}