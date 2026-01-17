using Domain.Common.Abstractions;
using Domain.Models.Auth.Users;
using Domain.Models.Employers;
using Domain.Models.Projects;

namespace Domain.Models.Freelance;

public class Contract : AuditableEntity<Guid>
{
    public required Guid ProjectId { get; set; }
    public Project? Project { get; set; }

    public required Guid EmployerId { get; set; }
    public Employer? Employer { get; set; }

    public required Guid FreelancerId { get; set; }
    public Freelancer? Freelancer { get; set; }

    public decimal Amount { get; set; }
    public string Status { get; set; } = nameof(ContractStatus.Draft);
}

public enum ContractStatus
{
    Draft,
    Active,
    Completed,
    Cancelled
}
