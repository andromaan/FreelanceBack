using Domain.Common.Abstractions;
using Domain.Models.Auth;
using Domain.Models.Countries;

namespace Domain.Models.Users;

public class User : AuditableEntity<Guid>
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? DisplayName { get; set; }
    
    public required int RoleId { get; set; }
    public Role? Role { get; set; }
    
    public int? CountryId { get; set; }
    public Country? Country { get; set; }
    
    public string? AvatarImg { get; set; }
    
    public string? ExternalProvider { get; set; }
    public string? ExternalProviderKey { get; set; }
    
    public string? StripeCustomerId { get; set; }
    
    public ICollection<UserLanguage> Languages { get; set; } = new List<UserLanguage>();
}
