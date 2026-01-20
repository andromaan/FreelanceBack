using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.FreelancerSkills;
using DAL.Data;
using Domain.Models.Freelance;

namespace DAL.Repositories;

public class FreelancerSkillRepository(AppDbContext appDbContext, IUserProvider userProvider)
    : Repository<FreelancerSkill, Guid>(appDbContext, userProvider), IFreelancerSkillRepository, IFreelancerSkillQueries
{
    
}

