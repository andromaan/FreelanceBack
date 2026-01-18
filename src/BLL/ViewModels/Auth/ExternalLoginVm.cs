namespace BLL.ViewModels.Auth;

public class ExternalLoginVm
{
    public required string Provider { get; set; }
    public required string Token { get; set; }
}