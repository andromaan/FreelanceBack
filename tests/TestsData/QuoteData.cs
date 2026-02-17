using Domain.Models.Projects;

namespace TestsData;

public class QuoteData
{
    public static Quote CreateQuote(
        Guid? id = null, 
        Guid? projectId = null, 
        Guid? freelancerId = null,
        decimal? amount = null,
        string? message = null)
    {
        return new Quote
        {
            Id = id ?? Guid.NewGuid(),
            ProjectId = projectId ?? Guid.NewGuid(),
            FreelancerId = freelancerId ?? Guid.NewGuid(),
            Amount = amount ?? 1500m,
            Message = message ?? "Test quote message"
        };
    }
}
