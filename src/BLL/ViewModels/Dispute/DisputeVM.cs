using Domain.Models.Disputes;

namespace BLL.ViewModels.Dispute;

public class DisputeVM
{
    public Guid Id { get; set; }
    public Guid ContractId { get; set; }
    public string? Reason { get; set; }
    public DisputeStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
}