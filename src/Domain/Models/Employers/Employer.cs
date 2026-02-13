using Domain.Common.Abstractions;

namespace Domain.Models.Employers;

public class Employer : AuditableEntity<Guid>
{
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyWebsite { get; set; } = string.Empty;
}