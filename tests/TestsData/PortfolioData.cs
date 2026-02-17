using Domain.Models.Freelance;

namespace TestsData;

public class PortfolioData
{
    public static Portfolio CreatePortfolio(
        Guid? id = null, 
        Guid? freelancerId = null, 
        Guid? userId = null,
        string? title = null,
        string? description = null,
        string? portfolioUrl = null)
    {
        return new Portfolio
        {
            Id = id ?? Guid.NewGuid(),
            FreelancerId = freelancerId ?? Guid.NewGuid(),
            CreatedBy = userId ?? Guid.NewGuid(),
            Title = title ?? "Test Portfolio Title",
            Description = description ?? "Test portfolio description with project details",
            PortfolioUrl = portfolioUrl ?? "https://example.com/portfolio"
        };
    }
}

