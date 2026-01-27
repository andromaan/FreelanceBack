using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.ProjectMilestones;
using DAL.Data;
using Domain.Models.Projects;

namespace DAL.Repositories;

public class ProjectMilestoneRepository(AppDbContext context, IUserProvider provider)
    : Repository<ProjectMilestone, Guid>(context, provider), IProjectMilestoneRepository, IProjectMilestoneQueries
{
}