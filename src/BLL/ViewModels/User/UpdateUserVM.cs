using BLL.Common.Interfaces;

namespace BLL.ViewModels.User;

public class UpdateUserVM : ISkipMapper
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? DisplayName { get; set; }
}