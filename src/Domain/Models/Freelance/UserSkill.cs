using Domain.Common.Abstractions;
using Domain.Models.Auth.Users;

namespace Domain.Models.Freelance;

public class UserSkill : Entity<Guid>
{
    public required Guid FreelancerInfoId { get; set; }
    public FreelancerInfo? FreelancerInfo { get; set; }

    public required int SkillId { get; set; }
    public Skill? Skill { get; set; }
}