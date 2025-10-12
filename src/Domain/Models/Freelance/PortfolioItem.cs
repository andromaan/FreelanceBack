using Domain.Common.Abstractions;
using Domain.Models.Auth.Users;

namespace Domain.Models.Freelance;

public class PortfolioItem : AuditableEntity<Guid>
{
    public required Guid UserProfileId { get; set; }
    public UserProfile? UserProfile { get; set; }

    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? FileUrl { get; set; }
}