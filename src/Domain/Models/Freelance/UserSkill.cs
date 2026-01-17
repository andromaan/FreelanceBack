using Domain.Common.Abstractions;
using Domain.Models.Auth.Users;

namespace Domain.Models.Freelance;

public class UserSkill : Entity<Guid>
{
    public required Guid FreelancerId { get; set; }
    public Freelancer? Freelancer { get; set; }

    public required int SkillId { get; set; }
    public Skill? Skill { get; set; }
}