using Domain.Common.Abstractions;
using Domain.Models.Auth;
using Domain.Models.Countries;
using Domain.Models.Languages;

namespace Domain.Models.Users;

public class User : AuditableEntity<Guid>
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    
    public required string RoleId { get; set; }
    public Role? Role { get; set; }
    
    public int? CountryId { get; set; }
    public Country? Country { get; set; }
    
    
    public string? AvatarImg { get; set; }
    
    public string? DisplayName { get; set; }
    public string? ExternalProvider { get; set; }
    public string? ExternalProviderKey { get; set; }
    public ICollection<Language> Languages { get; set; } = new List<Language>();
}
