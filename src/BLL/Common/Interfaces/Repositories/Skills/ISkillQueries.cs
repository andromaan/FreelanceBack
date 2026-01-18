using Domain.Models.Projects;

namespace BLL.Common.Interfaces.Repositories.Skills;

public interface ISkillQueries : IQueries<Skill, int>, IUniqueQuery<Skill, int>
{
}