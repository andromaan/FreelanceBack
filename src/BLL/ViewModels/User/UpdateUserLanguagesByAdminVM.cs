using BLL.Common.Interfaces;

namespace BLL.ViewModels.User;

public class UpdateUserLanguagesByAdminVM : ISkipMapper
{
    public List<int> LanguageIds { get; init; } = [];
}