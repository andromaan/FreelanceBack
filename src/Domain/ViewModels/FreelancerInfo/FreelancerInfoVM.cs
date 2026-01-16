using Domain.ViewModels.Country;
using Domain.ViewModels.Language;

namespace Domain.ViewModels.FreelancerInfo;

public class FreelancerInfoVM
{
    public string? Bio { get; set; }
    public decimal? HourlyRate { get; set; }
    public string? Location { get; set; }
    public CountryVM? Country { get; set; }
    public List<LanguageVM> Languages { get; set; } = [];
}