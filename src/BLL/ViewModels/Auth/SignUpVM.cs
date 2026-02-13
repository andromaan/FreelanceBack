namespace BLL.ViewModels.Auth;

public class SignUpVM
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? DisplayName { get; set; }
    public required string UserRole { get; set; }
}