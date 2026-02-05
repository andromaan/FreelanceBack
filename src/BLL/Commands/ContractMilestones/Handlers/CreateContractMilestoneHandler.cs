using BLL.Common.Handlers;
using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.ContractMilestones;
using BLL.Common.Interfaces.Repositories.Contracts;
using BLL.Services;
using BLL.ViewModels.ContractMilestone;
using Domain;
using Domain.Models.Freelance;

namespace BLL.Commands.ContractMilestones.Handlers;

public class CreateContractMilestoneHandler(
    IUserProvider userProvider,
    IContractQueries contractQueries,
    IContractMilestoneQueries milestoneQueries
) : ICreateHandler<ContractMilestone, CreateContractMilestoneVM>
{
    public async Task<Result<ContractMilestone, ServiceResponse>> HandleAsync(ContractMilestone? entity,
        CreateContractMilestoneVM createModel, CancellationToken cancellationToken)
    {
        var userRole = userProvider.GetUserRole();
        var userId = await userProvider.GetUserId();

        var existingContract = await contractQueries.GetByIdAsync(createModel.ContractId, cancellationToken);

        if (existingContract is null)
        {
            return Result<ContractMilestone, ServiceResponse>.Failure(
                ServiceResponse.NotFound($"Contract with Id {createModel.ContractId} not found"));
        }

        if (existingContract.CreatedBy != userId && userRole != Settings.Roles.AdminRole)
        {
            return Result<ContractMilestone, ServiceResponse>.Failure(
                ServiceResponse.Unauthorized("You are not authorized to create a milestone for this contract"));
        }

        var existingMilestones =
            await milestoneQueries.GetByContractIdAsync(createModel.ContractId, cancellationToken);

        var totalMilestoneAmount = existingMilestones.Sum(x => x.Amount) + createModel.Amount;
        if (totalMilestoneAmount > existingContract.AgreedRate)
        {
            return Result<ContractMilestone, ServiceResponse>.Failure(ServiceResponse.GetResponse(
                $"The total amount ({totalMilestoneAmount}) of milestones exceeds " +
                $"the contract's agreed rate ({existingContract.AgreedRate})",
                false, null, System.Net.HttpStatusCode.BadRequest));
        }

        return Result<ContractMilestone, ServiceResponse>.Success(null); // Валідація пройшла успішно
    }
}