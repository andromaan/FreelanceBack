using Domain.Models.Disputes;

namespace TestsData;

public class DisputeResolutionData
{
    public static DisputeResolution CreateDisputeResolution(
        Guid? id = null,
        Guid? disputeId = null,
        string? resolutionDetails = null,
        Guid? createdById = null)
    {
        return new DisputeResolution
        {
            Id = id ?? Guid.NewGuid(),
            DisputeId = disputeId ?? Guid.NewGuid(),
            ResolutionDetails = resolutionDetails ?? "Default resolution details - dispute resolved in favor of client",
            CreatedBy = createdById ?? Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow
        };
    }
}
