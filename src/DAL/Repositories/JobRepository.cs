using BLL.Common.Interfaces.Repositories;
using DAL.Data;
using Domain.Common.Interfaces;
using Domain.Models.Freelance;

namespace DAL.Repositories;

public class JobRepository(AppDbContext appDbContext, IUserProvider userProvider)
    : Repository<Job, Guid>(appDbContext, userProvider), IJobRepository
{
    
}