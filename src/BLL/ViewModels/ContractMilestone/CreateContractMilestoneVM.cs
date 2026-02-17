namespace BLL.ViewModels.ContractMilestone;

public class CreateContractMilestoneVM
{
    public Guid ContractId { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
}