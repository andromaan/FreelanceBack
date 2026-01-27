using Domain.Models.Freelance;

namespace BLL.Common.Interfaces.Repositories.Freelancers;

public interface IFreelancerQueries : IQueries<Freelancer, Guid>
{
    Task<Freelancer?> GetByUserIdAsync(Guid userId, CancellationToken token, bool includes = false);
}