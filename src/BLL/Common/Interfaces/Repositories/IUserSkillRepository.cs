using Domain.Models.Freelance;

namespace BLL.Common.Interfaces.Repositories;

public interface IUserSkillRepository : IRepository<UserSkill, Guid>, IQueries<UserSkill, Guid>
{
    
}

