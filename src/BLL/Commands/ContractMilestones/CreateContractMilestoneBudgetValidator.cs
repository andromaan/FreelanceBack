using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.ContractMilestones;
using BLL.Common.Interfaces.Repositories.Contracts;
using BLL.Common.Validators;
using BLL.Services;
using BLL.ViewModels.ContractMilestone;
using Domain;

namespace BLL.Commands.ContractMilestones;

public class CreateContractMilestoneBudgetValidator(
    IContractQueries contractQueries,
    IContractMilestoneQueries milestoneQueries,
    IUserProvider userProvider) 
    : ICreateValidator<CreateContractMilestoneVM>
{
    public async Task<ServiceResponse?> ValidateAsync(
        CreateContractMilestoneVM createModel,
        CancellationToken cancellationToken)
    {
        var userRole = userProvider.GetUserRole();
        var userId = await userProvider.GetUserId();
        
        var existingContract = await contractQueries.GetByIdAsync(createModel.ContractId, cancellationToken);

        if (existingContract is null)
        {
            return ServiceResponse.NotFound($"Contract with Id {createModel.ContractId} not found");
        }

        if (existingContract.CreatedBy != userId && userRole != Settings.Roles.AdminRole)
        {
            return ServiceResponse.Unauthorized("You are not authorized to create a milestone for this contract");
        }

        var existingMilestones =
            await milestoneQueries.GetByContractIdAsync(createModel.ContractId, cancellationToken);

        var totalMilestoneAmount = existingMilestones.Sum(x => x.Amount) + createModel.Amount;
        if (totalMilestoneAmount > existingContract.AgreedRate)
        {
            return ServiceResponse.GetResponse(
                $"The total amount ({totalMilestoneAmount}) of milestones exceeds " +
                $"the contract's agreed rate ({existingContract.AgreedRate})",
                false, null, System.Net.HttpStatusCode.BadRequest);
        }

        return null; // Валідація пройшла успішно
    }
}