using Domain.Common.Abstractions;
using Domain.Models.Freelance;

namespace Domain.Models.Reviews;

public class Review : AuditableEntity<Guid>
{
    public Guid ContractId { get; set; }
    public Guid ReviewedUserId { get; set; }
    public Contract Contract { get; set; } = null!;
    public decimal Rating { get; set; }
    public string ReviewText { get; set; } = string.Empty;
    public string ReviewerRoleId { get; set; } = null!;
}