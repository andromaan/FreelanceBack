using BLL.Common.Interfaces;
using Domain.Models.Contracts;

namespace BLL.ViewModels.ContractMilestone;

public class UpdContractMilestoneStatusFreelancerVM : ISkipAuditable
{
    public ContractMilestoneFreelancerStatus Status { get; set; }
}

public enum ContractMilestoneFreelancerStatus
{
    InProgress = ContractMilestoneStatus.InProgress,
    Submitted = ContractMilestoneStatus.Submitted,
}