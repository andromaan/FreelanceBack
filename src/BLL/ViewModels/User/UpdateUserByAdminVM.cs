using BLL.Common.Interfaces;

namespace BLL.ViewModels.User;

public class UpdateUserByAdminVM : ISkipMapper
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? DisplayName { get; set; }
    public int? CountryId { get; set; }
}