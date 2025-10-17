namespace Domain.ViewModels.Auth;

public class SignUpVm
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string? DisplayName { get; set; }
    public bool IsFreelancer { get; set; }
}