using Domain.Common.Abstractions;
using Domain.Models.Projects;

namespace Domain.Models.Freelance;

public class Contract : AuditableEntity<Guid>
{
    public required Guid ProjectId { get; set; }
    public Project? Project { get; set; }

    public required Guid FreelancerId { get; set; }
    public Freelancer? Freelancer { get; set; }
    
    // Employer is in Project as CreatedBy
    
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public decimal Amount { get; set; }
    public string Status { get; set; } = nameof(ContractStatus.Pending);
}

public enum ContractStatus
{
    Pending,
    Active,
    Completed,
    Cancelled,
    InProgress,
    Disputed,
    Refunded
}
