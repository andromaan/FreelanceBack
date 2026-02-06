using BLL.Common.Handlers;
using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.ContractMilestones;
using BLL.Common.Interfaces.Repositories.Contracts;
using BLL.Common.Interfaces.Repositories.Freelancers;
using BLL.Services;
using BLL.ViewModels.ContractMilestone;
using Domain.Models.Freelance;

namespace BLL.Commands.ContractMilestones.Handlers;

/// <summary>
/// Unified handler for ContractMilestone status update that combines validation and processing.
/// Replaces UpdateContractMilestoneStatusValidator.
/// </summary>
public class UpdateContractMilestoneStatusFreelancerHandler(
    IUserProvider userProvider,
    IContractQueries contractQueries,
    IFreelancerQueries freelancerQueries,
    IContractMilestoneQueries contractMilestoneQueries,
    IContractRepository contractRepository
)
    : IUpdateHandler<ContractMilestone, UpdContractMilestoneStatusFreelancerVM>
{
    public async Task<ServiceResponse?> HandleAsync(
        ContractMilestone existingEntity,
        UpdContractMilestoneStatusFreelancerVM updateModel,
        CancellationToken cancellationToken)
    {
        // Перевірка прав доступу
        var userId = await userProvider.GetUserId();
        var contract = await contractQueries.GetByIdAsync(existingEntity.ContractId, cancellationToken);
        var freelancer = await freelancerQueries.GetByUserIdAsync(userId, cancellationToken);

        if (contract!.FreelancerId != freelancer!.Id)
        {
            return ServiceResponse.Forbidden("You do not have permission to edit this entity");
        }

        // Processing: Update contract status if first milestone is in progress
        var contractStatusChangeResult =
            await UpdateContractStatusIfNeeded(existingEntity, contract, updateModel, cancellationToken);
        if (contractStatusChangeResult != null)
            return contractStatusChangeResult;


        existingEntity.Status = (ContractMilestoneStatus)updateModel.Status;

        return ServiceResponse.Ok(); // Валідація пройшла успішно
    }

    private async Task<ServiceResponse?> UpdateContractStatusIfNeeded(ContractMilestone existingEntity,
        Contract contract, UpdContractMilestoneStatusFreelancerVM updateModel, CancellationToken cancellationToken)
    {
        var contractMilestonesByContract =
            (await contractMilestoneQueries.GetByContractIdAsync(existingEntity.ContractId, cancellationToken))
            .ToList();

        if (contractMilestonesByContract.All(m => m.Status == ContractMilestoneStatus.Pending)
            && updateModel.Status == ContractMilestoneFreelancerStatus.InProgress)
        {
            contract.Status = ContractStatus.InProgress;

            try
            {
                await contractRepository.UpdateAsync(contract, cancellationToken);
            }
            catch (Exception e)
            {
                return ServiceResponse.InternalError(e.Message, e.InnerException);
            }
        }
        
        return null;
    }
}