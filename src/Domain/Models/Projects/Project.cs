using Domain.Common.Abstractions;

namespace Domain.Models.Projects;

public class Project : AuditableEntity<Guid>
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    
    public decimal Budget { get; set; }
    public ProjectStatus Status { get; set; } = ProjectStatus.Open;
    public DateTime Deadline { get; set; }

    public ICollection<Bid> Bids { get; set; } = new List<Bid>();
    public ICollection<Quote> Quotes { get; set; } = new List<Quote>();
    public ICollection<Category> Categories { get; set; } = new List<Category>();
}

public enum ProjectStatus
{
    Open,
    InProgress,
    Completed,
    Cancelled
}
