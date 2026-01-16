using Domain.Common.Abstractions;
using Domain.Models.Auth.Users;
using Domain.Models.Countries;
using Domain.Models.Languages;

namespace Domain.Models.Freelance;

public class FreelancerInfo : AuditableEntity<Guid>
{
    public required Guid UserId { get; set; }
    public User? User { get; set; }

    public string? Bio { get; set; }
    public decimal? HourlyRate { get; set; }
    public string? Location { get; set; }
    
    public string? AvatarLogo { get; set; }
    
    public int? CountryId { get; set; }
    public Country? Country { get; set; }
    
    public ICollection<Language> Languages { get; set; } = new List<Language>();
    public ICollection<UserSkill> Skills { get; set; } = new List<UserSkill>();
    public ICollection<PortfolioItem> Portfolio { get; set; } = new List<PortfolioItem>();
}