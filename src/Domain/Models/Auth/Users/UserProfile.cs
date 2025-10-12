using Domain.Common.Abstractions;
using Domain.Models.Freelance;

namespace Domain.Models.Auth.Users;

public class UserProfile : AuditableEntity<Guid>
{
    public required Guid UserId { get; set; }
    public User? User { get; set; }

    public string? Bio { get; set; }
    public decimal? HourlyRate { get; set; }
    public string? Location { get; set; }
    
    public string? AvatarLogo { get; set; }
    
    public ICollection<UserSkill> Skills { get; set; } = new List<UserSkill>();
    public ICollection<PortfolioItem> Portfolio { get; set; } = new List<PortfolioItem>();
}