using Domain.Models.Freelance;

namespace BLL.Common.Interfaces.Repositories.Freelancers;

public interface IFreelancerRepository : IRepository<Freelancer, Guid>
{
    public Task<Freelancer?> CreateAsync(Freelancer entity, Guid createdBy, CancellationToken token);
}