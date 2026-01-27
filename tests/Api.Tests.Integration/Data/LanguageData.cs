using Domain.Models.Languages;

namespace Api.Tests.Integration.Data;

public class LanguageData
{
    public static Language MainLanguage => new()
    {
        Id = 0,
        Name = "MainLanguage",
        Code = "MA",
    };
}