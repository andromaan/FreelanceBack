namespace BLL.ViewModels.User;

public class CreateUserByAdminVM
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public string? DisplayName { get; set; }
}