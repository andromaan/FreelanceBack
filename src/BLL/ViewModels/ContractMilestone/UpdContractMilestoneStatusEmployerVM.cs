using BLL.Common.Interfaces;
using Domain.Models.Contracts;

namespace BLL.ViewModels.ContractMilestone;

public class UpdContractMilestoneStatusEmployerVM : ISkipMapper
{
    public ContractMilestoneEmployerStatus Status { get; set; }
}

public enum ContractMilestoneEmployerStatus
{
    UnderReview = ContractMilestoneStatus.UnderReview,
    Approved = ContractMilestoneStatus.Approved,
    InProgress = ContractMilestoneStatus.InProgress,
}