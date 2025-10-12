namespace DAL.Data.Initializer;

public static partial class DataSeed
{
    private class CountryDto
    {
        public string name { get; set; } = string.Empty;
        public string alpha2 { get; set; } = string.Empty;
        public string alpha3 { get; set; } = string.Empty;
        public string unicode { get; set; } = string.Empty;
        public string emoji { get; set; } = string.Empty;
        public string dialCode { get; set; } = string.Empty;
        public string region { get; set; } = string.Empty;
        public string capital { get; set; } = string.Empty;
    }
}