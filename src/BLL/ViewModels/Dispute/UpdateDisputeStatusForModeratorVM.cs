using BLL.Common.Interfaces;
using Domain.Models.Disputes;

namespace BLL.ViewModels.Dispute;

public class UpdateDisputeStatusForModeratorVM : ISkipAuditable, ISkipMapper
{
    public DisputeStatusForModerator Status { get; set; }
}

public enum DisputeStatusForModerator
{
    Open = DisputeStatus.Open,
    UnderReview = DisputeStatus.UnderReview,
}