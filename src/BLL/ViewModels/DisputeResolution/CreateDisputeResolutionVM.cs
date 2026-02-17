using Domain.Models.Disputes;

namespace BLL.ViewModels.DisputeResolution;

public class CreateDisputeResolutionVM
{
    public Guid DisputeId { get; set; }
    public string ResolutionDetails { get; set; } = string.Empty;
    public DisputeResolutionStatusForModerator DisputeStatus { get; set; }
}

public enum DisputeResolutionStatusForModerator
{
    Resolved = DisputeStatus.Resolved,
    Rejected = DisputeStatus.Rejected
}