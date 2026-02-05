using BLL.Common.Handlers;
using BLL.Common.Interfaces;
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
    IFreelancerQueries freelancerQueries
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
        
        existingEntity.Status = (ContractMilestoneStatus)updateModel.Status;
        
        return ServiceResponse.Ok();  // Валідація пройшла успішно
    }
}