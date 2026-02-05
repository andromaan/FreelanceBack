using System.Net;
using BLL.Common.Interfaces.Repositories.UserWallets;
using BLL.Common.Validators;
using BLL.Services;
using BLL.ViewModels.ContractMilestone;
using Domain.Models.Freelance;

namespace BLL.Commands.ContractMilestones.Validators;

public class UpdateContractMilestoneStatusValidator(
    IUserWalletQueries userWalletQueries)
    : IUpdateValidator<ContractMilestone, UpdContractMilestoneStatusEmployerVM>
{
    public async Task<ServiceResponse?> ValidateAsync(
        ContractMilestone existingMilestone,
        UpdContractMilestoneStatusEmployerVM updateModel,
        CancellationToken cancellationToken)
    {
        if (existingMilestone.Status == ContractMilestoneStatus.Approved)
        {
            return ServiceResponse.GetResponse(
                "Cannot change the status of an approved contract milestone.",
                false, null, HttpStatusCode.BadRequest);
        }

        if (updateModel.Status == ContractMilestoneEmployerStatus.Approved)
        {
            if (!(await userWalletQueries.IsWithdrawSuccessAsync(existingMilestone.CreatedBy, existingMilestone.Amount,
                    cancellationToken)))
            {
                return ServiceResponse.GetResponse(
                    "Insufficient funds in the employer's wallet to approve this milestone.",
                    false, null, HttpStatusCode.BadRequest);
            }
        }

        return null; // Валідація пройшла успішно
    }
}