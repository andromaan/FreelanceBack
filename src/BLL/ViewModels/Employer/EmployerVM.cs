namespace BLL.ViewModels.Employer;

public class EmployerVM
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string CompanyWebsite { get; set; } = string.Empty;
}