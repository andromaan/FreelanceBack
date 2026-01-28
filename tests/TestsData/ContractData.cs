using Domain.Models.Freelance;

namespace TestsData;

public class ContractData
{
    public static Contract CreateContract(Guid? id = null, Guid? projectId = null, Guid? freelancerId = null)
    {
        return new Contract
        {
            Id = id ?? Guid.NewGuid(),
            ProjectId = projectId ?? Guid.NewGuid(),
            FreelancerId = freelancerId ?? Guid.NewGuid(),
            Amount = 1000m,
            Status = "Pending",
            StartDate = DateTime.UtcNow
        };
    }
}
