namespace BLL.ViewModels.Freelancer;

public class UpdateFreelancerVM
{
    public string Bio { get; set; } = string.Empty;
    public decimal HourlyRate { get; set; }
    public string Location { get; set; } = string.Empty;
    public int CountryId { get; set; }
}