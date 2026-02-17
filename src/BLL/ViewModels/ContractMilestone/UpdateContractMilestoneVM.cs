using BLL.Common.Interfaces;

namespace BLL.ViewModels.ContractMilestone;

public class UpdateContractMilestoneVM : ISkipMapper
{
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
}