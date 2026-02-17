using BLL.ViewModels.Country;
using BLL.ViewModels.Language;

namespace BLL.ViewModels.User;

public class UserVM
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string RoleId { get; set; } = string.Empty;
    public string? AvatarImg { get; set; }
    public CountryVM? Country { get; set; }
    public List<LanguageVM> Languages { get; set; } = [];
    public string? DisplayName { get; set; }
    public DateTime CreatedAt { get; set; }
}