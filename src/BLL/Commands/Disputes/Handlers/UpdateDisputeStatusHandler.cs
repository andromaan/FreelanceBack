using BLL.Common.Handlers;
using BLL.Services;
using BLL.ViewModels.Dispute;
using Domain.Models.Disputes;

namespace BLL.Commands.Disputes.Handlers;

public class UpdateDisputeStatusHandler : IUpdateHandler<Dispute, UpdateDisputeStatusForModeratorVM>
{
    public Task<ServiceResponse?> HandleAsync(Dispute existingEntity, UpdateDisputeStatusForModeratorVM updateModel,
        CancellationToken cancellationToken)
    {
        existingEntity.Status = (DisputeStatus)updateModel.Status;

        return Task.FromResult<ServiceResponse?>(ServiceResponse.Ok());
    }
}