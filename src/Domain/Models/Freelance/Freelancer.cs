using Domain.Common.Abstractions;
using Domain.Models.Countries;
using Domain.Models.Languages;
using Domain.Models.Projects;

namespace Domain.Models.Freelance;

public class Freelancer : AuditableEntity<Guid>
{
    public string? Bio { get; set; }
    public string? Location { get; set; }
    
    public string? AvatarLogo { get; set; }
    
    public int? CountryId { get; set; }
    public Country? Country { get; set; }
    
    public ICollection<Language> Languages { get; set; } = new List<Language>();
    public ICollection<Skill> Skills { get; set; } = new List<Skill>();
    public ICollection<Portfolio> Portfolio { get; set; } = new List<Portfolio>();
}