using Domain.Models.Projects;

namespace BLL.Common.Interfaces.Repositories.ProjectMilestones;

public interface IProjectMilestoneQueries : IQueries<ProjectMilestone, Guid>
{
    Task<IEnumerable<ProjectMilestone>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default);
}