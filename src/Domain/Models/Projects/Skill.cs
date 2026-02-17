using Domain.Common.Abstractions;

namespace Domain.Models.Projects;

public class Skill : Entity<int>
{
    public required string Name { get; set; }
}