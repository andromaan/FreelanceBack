namespace BLL.ViewModels.Portfolio;

public class PortfolioVM
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? PortfolioUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}