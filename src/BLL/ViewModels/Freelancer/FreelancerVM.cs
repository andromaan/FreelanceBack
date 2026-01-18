using BLL.ViewModels.Country;
using BLL.ViewModels.Language;

namespace BLL.ViewModels.Freelancer;

public class FreelancerVM
{
    public string? Bio { get; set; }
    public decimal? HourlyRate { get; set; }
    public string? Location { get; set; }
    public CountryVM? Country { get; set; }
    public List<LanguageVM> Languages { get; set; } = [];
}