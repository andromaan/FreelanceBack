using Domain.Common.Abstractions;

namespace Domain.Models.Freelance;

public class Skill : Entity<int>
{
    public required string Name { get; set; }
}