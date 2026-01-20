namespace BLL.ViewModels;

public class JwtVM
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}