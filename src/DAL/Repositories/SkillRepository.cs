using BLL.Common.Interfaces.Repositories.Skills;
using DAL.Data;
using Domain.Common.Interfaces;
using Domain.Models.Freelance;

namespace DAL.Repositories;

public class SkillRepository(AppDbContext appDbContext, IUserProvider userProvider)
    : Repository<Skill, int>(appDbContext, userProvider), ISkillRepository, ISkillQueries
{
    
}

