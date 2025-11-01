using BLL.Common.Interfaces.Repositories;
using DAL.Data;
using Domain.Common.Interfaces;
using Domain.Models.Freelance;

namespace DAL.Repositories;

public class UserSkillRepository(AppDbContext appDbContext, IUserProvider userProvider)
    : Repository<UserSkill, Guid>(appDbContext, userProvider), IUserSkillRepository
{
    
}

