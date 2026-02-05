using Domain.Models.Freelance;

namespace BLL.ViewModels.ContractMilestone;

public class UpdContractMilestoneStatusEmployerVM
{
    public ContractMilestoneEmployerStatus Status { get; set; }
}

public enum ContractMilestoneEmployerStatus
{
    UnderReview = ContractMilestoneStatus.UnderReview,
    Approved = ContractMilestoneStatus.Approved,
    Rejected = ContractMilestoneStatus.Rejected,
}