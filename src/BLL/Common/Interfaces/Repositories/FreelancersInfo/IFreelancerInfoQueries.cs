using Domain.Models.Freelance;

namespace BLL.Common.Interfaces.Repositories.FreelancersInfo;

public interface IFreelancerInfoQueries : IQueries<FreelancerInfo, Guid>
{
    Task<FreelancerInfo?> GetByUserId(Guid userId, CancellationToken token, bool includes = false);
}