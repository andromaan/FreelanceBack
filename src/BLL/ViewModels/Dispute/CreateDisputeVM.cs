namespace BLL.ViewModels.Dispute;

public class CreateDisputeVM
{
    public Guid ContractId { get; set; }
    public string? Reason { get; set; }
}