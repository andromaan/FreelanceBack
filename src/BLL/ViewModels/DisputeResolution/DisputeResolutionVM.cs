namespace BLL.ViewModels.DisputeResolution;

public class DisputeResolutionVM
{
    public Guid Id { get; set; }
    public Guid DisputeId { get; set; }
    public Guid ResolvedById { get; set; }
    public string ResolutionDetails { get; set; } = string.Empty;
    public DateTime ResolutionDate { get; set; }
}