using Domain.Common.Abstractions;

namespace Domain.Models.Freelance;

public class Portfolio : AuditableEntity<Guid>
{
    public required Guid FreelancerId { get; set; }
    public Freelancer? Freelancer { get; set; }
    
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? PortfolioUrl { get; set; }
}