using Domain.Models.Contracts;

namespace TestsData;

public class ContractData
{
    public static Contract CreateContract(Guid? id = null, Guid? projectId = null, Guid? freelancerId = null,
        decimal? agreedRate = null, Guid? createdById = null)
    {
        return new Contract
        {
            Id = id ?? Guid.NewGuid(),
            ProjectId = projectId ?? Guid.NewGuid(),
            FreelancerId = freelancerId ?? Guid.NewGuid(),
            AgreedRate = agreedRate ?? 1000m,
            Status = ContractStatus.Pending,
            StartDate = DateTime.UtcNow,
            CreatedBy = createdById ?? Guid.NewGuid()
        };
    }
}