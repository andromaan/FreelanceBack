using System.Net;
using BLL.Common.Handlers;
using BLL.Common.Interfaces.Repositories.UserWallets;
using BLL.Services;
using BLL.ViewModels.ContractMilestone;
using Domain.Models.Freelance;

namespace BLL.Commands.ContractMilestones.Handlers;

/// <summary>
/// Unified handler for ContractMilestone status update that combines validation and processing.
/// Replaces UpdateContractMilestoneStatusValidator.
/// </summary>
public class UpdateContractMilestoneStatusHandler(
    IUserWalletQueries userWalletQueries)
    : IUpdateHandler<ContractMilestone, UpdContractMilestoneStatusEmployerVM>
{
    public async Task<Result<ContractMilestone, ServiceResponse>> HandleAsync(
        ContractMilestone existingEntity,
        ContractMilestone mappedEntity,
        UpdContractMilestoneStatusEmployerVM updateModel,
        CancellationToken cancellationToken)
    {
        // Validation: Check if milestone is already approved
        if (existingEntity.Status == ContractMilestoneStatus.Approved)
        {
            return Result<ContractMilestone, ServiceResponse>.Failure(
                ServiceResponse.GetResponse(
                    "Cannot change the status of an approved contract milestone.",
                    false, null, HttpStatusCode.BadRequest));
        }

        // Validation: Check wallet balance if approving
        if (updateModel.Status == ContractMilestoneEmployerStatus.Approved)
        {
            if (!(await userWalletQueries.IsWithdrawSuccessAsync(
                existingEntity.CreatedBy, 
                existingEntity.Amount,
                cancellationToken)))
            {
                return Result<ContractMilestone, ServiceResponse>.Failure(
                    ServiceResponse.GetResponse(
                        "Insufficient funds in the employer's wallet to approve this milestone.",
                        false, null, HttpStatusCode.BadRequest));
            }
        }

        // Processing: No additional processing needed, return mapped entity
        return Result<ContractMilestone, ServiceResponse>.Success(mappedEntity);
    }
}
