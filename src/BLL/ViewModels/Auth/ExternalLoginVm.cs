namespace BLL.ViewModels.Auth;

public class ExternalLoginVM
{
    public required string Provider { get; set; }
    public required string Token { get; set; }
    public string? UserRole { get; set; }
}