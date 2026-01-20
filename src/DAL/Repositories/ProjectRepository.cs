using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Projects;
using DAL.Data;
using Domain.Models.Projects;

namespace DAL.Repositories;

public class ProjectRepository(AppDbContext appDbContext, IUserProvider userProvider)
    : Repository<Project, Guid>(appDbContext, userProvider), IProjectRepository, IProjectQueries
{
    public override Task<IEnumerable<Project>> GetAllAsync(CancellationToken token)
    {
        return base.GetAllAsync(token, p => p.Categories,
            p => p.Proposals);
    }

    public override Task<Project?> GetByIdAsync(Guid id, CancellationToken token, bool asNoTracking = false)
    {
        return base.GetByIdAsync(id, token, asNoTracking,
            p => p.Categories,
            p => p.Proposals);
    }
}