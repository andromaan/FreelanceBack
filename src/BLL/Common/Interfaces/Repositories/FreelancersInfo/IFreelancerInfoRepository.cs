using Domain.Models.Freelance;

namespace BLL.Common.Interfaces.Repositories.FreelancersInfo;

public interface IFreelancerInfoRepository : IRepository<FreelancerInfo, Guid>
{
    public Task<FreelancerInfo?> CreateAsync(FreelancerInfo entity, Guid createdBy, CancellationToken token);
}