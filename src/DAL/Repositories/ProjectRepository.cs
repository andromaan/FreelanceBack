using BLL.Common.Interfaces.Repositories.Projects;
using DAL.Data;
using Domain.Common.Interfaces;
using Domain.Models.Freelance;
using Domain.Models.Projects;

namespace DAL.Repositories;

public class ProjectRepository(AppDbContext appDbContext, IUserProvider userProvider)
    : Repository<Project, Guid>(appDbContext, userProvider), IProjectRepository, IProjectQueries
{
    
}