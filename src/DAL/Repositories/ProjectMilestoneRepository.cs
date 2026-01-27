using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.ProjectMilestones;
using DAL.Data;
using Domain.Models.Projects;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;

public class ProjectMilestoneRepository(AppDbContext context, IUserProvider provider)
    : Repository<ProjectMilestone, Guid>(context, provider), IProjectMilestoneRepository, IProjectMilestoneQueries
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<ProjectMilestone>> GetByProjectIdAsync(Guid projectId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<ProjectMilestone>().Where(x => x.ProjectId == projectId)
            .ToListAsync(cancellationToken);
    }
}