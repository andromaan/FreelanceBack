namespace BLL.ViewModels.Portfolio;

public class UpdatePortfolioVM
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? PortfolioUrl { get; set; }
}