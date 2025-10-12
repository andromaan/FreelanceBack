using Domain.Common.Abstractions;

namespace Domain.Models.Auth.Users;

public class User : AuditableEntity<Guid>
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    
    public required string RoleId { get; set; }
    public Role? Role { get; set; }
    
    public string? AvatarImg { get; set; }
    
    public string? DisplayName { get; set; }
    public string? ExternalProvider { get; set; }
    public string? ExternalProviderKey { get; set; }
}