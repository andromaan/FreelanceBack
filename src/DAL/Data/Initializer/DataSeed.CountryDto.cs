namespace DAL.Data.Initializer;

public static partial class DataSeed
{
    private class CountryDto
    {
        public string Name { get; set; } = string.Empty;
        public string Alpha2 { get; set; } = string.Empty;
        public string Alpha3 { get; set; } = string.Empty;
        // public string Unicode { get; set; } = string.Empty;
        // public string Emoji { get; set; } = string.Empty;
        // public string DialCode { get; set; } = string.Empty;
        // public string Region { get; set; } = string.Empty;
        // public string Capital { get; set; } = string.Empty;
    }
}