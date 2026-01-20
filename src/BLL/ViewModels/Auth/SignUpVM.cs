namespace BLL.ViewModels.Auth;

public class SignUpVM
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? DisplayName { get; set; }
    public bool IsFreelancer { get; set; }
}