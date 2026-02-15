using BLL.Common.Interfaces;

namespace BLL.ViewModels.User;

public class UpdateUserLanguagesVM : ISkipMapper
{
    public List<int> LanguageIds { get; init; } = [];
}