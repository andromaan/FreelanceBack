using BLL.Common.Interfaces;
using Domain.Models.Contracts;

namespace BLL.ViewModels.ContractMilestone;

public class UpdContractMilestoneStatusModeratorVM : ISkipMapper
{
    public ContractMilestoneStatus Status { get; set; }
}