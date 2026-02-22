using Domain.Models.Contracts;

namespace TestsData;

public class ContractMilestoneData
{
    public static ContractMilestone MainContractMilestone(Guid? id = null, Guid? contractId = null,
        string description = "", decimal amount = 0, DateTime? dueDate = null,
        ContractMilestoneStatus status = ContractMilestoneStatus.InProgress, Guid? createdBy = null) => new()
    {
        Id = id ?? Guid.NewGuid(),
        ContractId = contractId ?? Guid.NewGuid(),
        Description = description,
        Amount = amount,
        DueDate = dueDate ?? DateTime.UtcNow.AddDays(30),
        Status = status,
        CreatedBy = createdBy ?? Guid.NewGuid(),
    };
}