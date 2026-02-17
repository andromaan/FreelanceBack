using Domain.Models.Disputes;

namespace TestsData;

public class DisputeData
{
    public static Dispute CreateDispute(
        Guid? id = null,
        Guid? contractId = null,
        string? reason = null,
        DisputeStatus? status = null,
        Guid? createdById = null)
    {
        return new Dispute
        {
            Id = id ?? Guid.NewGuid(),
            ContractId = contractId ?? Guid.NewGuid(),
            Reason = reason ?? "Default dispute reason",
            Status = status ?? DisputeStatus.Open,
            CreatedBy = createdById ?? Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow
        };
    }
}
