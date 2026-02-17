using Domain.Models.Languages;

namespace TestsData;

public class LanguageData
{
    public static Language MainLanguage => new()
    {
        Id = 0,
        Name = "MainLanguage",
        Code = "MA",
    };
}