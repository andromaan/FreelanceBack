namespace BLL.ViewModels.ContractMilestone;

public class ContractMilestoneVM
{
    public Guid Id { get; set; }
    public Guid ContractId { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
    public string Status { get; set; } = string.Empty;
}