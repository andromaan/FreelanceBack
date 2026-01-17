using Domain.Common.Abstractions;

namespace Domain.Models.Projects;

public class Category : AuditableEntity<Guid>
{
    public string? Name { get; set; }
}