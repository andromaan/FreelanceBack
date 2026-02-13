using Domain.Common.Abstractions;

namespace Domain.Models.Freelance;

public class Portfolio : AuditableEntity<Guid>
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? PortfolioUrl { get; set; }
}