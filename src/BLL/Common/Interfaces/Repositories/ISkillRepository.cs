using Domain.Models.Freelance;

namespace BLL.Common.Interfaces.Repositories;

public interface ISkillRepository : IRepository<Skill, int>, IQueries<Skill, int>
{
    
}

