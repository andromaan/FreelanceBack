namespace Domain.ViewModels.UserProfile;

public class UpdateProfileVM
{
    public string? Bio { get; set; }
    public decimal? HourlyRate { get; set; }
    public string? Location { get; set; }
    public int? CountryId { get; set; }
    public List<int>? LanguageIds { get; set; }
}