using Domain.Common.Abstractions;

namespace Domain.Models.Disputes;

public class DisputeResolution : AuditableEntity<Guid>
{
    public Guid ResolutionId { get; set; }
    public string ResolutionDetails { get; set; } = string.Empty;
    
}