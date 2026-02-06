using Domain.Common.Abstractions;
using Domain.Models.Auth;

namespace Domain.Models.Users;

public class User : AuditableEntity<Guid>
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    
    public required string RoleId { get; set; }
    public Role? Role { get; set; }
    
    public string? AvatarImg { get; set; }
    
    public string? DisplayName { get; set; }
    public string? ExternalProvider { get; set; }
    public string? ExternalProviderKey { get; set; }
}