using Domain.Models.Freelance;

namespace BLL.ViewModels.ContractMilestone;

public class UpdContractMilestoneStatusFreelancerVM
{
    public ContractMilestoneFreelancerStatus Status { get; set; }
}

public enum ContractMilestoneFreelancerStatus
{
    InProgress = ContractMilestoneStatus.InProgress,
    Submitted = ContractMilestoneStatus.Submitted,
}