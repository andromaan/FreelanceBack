using Domain.Models.Reviews;

namespace TestsData;

public class ReviewData
{
    public static Review CreateReview(
        Guid? id = null,
        Guid? contractId = null,
        Guid? reviewedUserId = null,
        decimal? rating = null,
        string? reviewText = null,
        string? reviewerRoleId = null,
        Guid? createdById = null)
    {
        return new Review
        {
            Id = id ?? Guid.NewGuid(),
            ContractId = contractId ?? Guid.NewGuid(),
            ReviewedUserId = reviewedUserId ?? Guid.NewGuid(),
            Rating = rating ?? 4.5m,
            ReviewText = reviewText ?? "Great work!",
            ReviewerRoleId = reviewerRoleId ?? "Employer",
            CreatedBy = createdById ?? Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow
        };
    }
}
