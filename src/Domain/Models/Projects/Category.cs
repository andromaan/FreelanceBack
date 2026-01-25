using Domain.Common.Abstractions;

namespace Domain.Models.Projects;

public class Category : Entity<int>
{
    public string? Name { get; set; }
}