using BLL.Common.Handlers;
using BLL.Common.Interfaces.Repositories.ContractMilestones;
using BLL.Common.Interfaces.Repositories.Contracts;
using BLL.Services;
using BLL.ViewModels.ContractMilestone;
using Domain.Models.Freelance;

namespace BLL.Commands.ContractMilestones.Handlers;

public class UpdateContractMilestoneBudgeHandler(
    IContractQueries contractQueries,
    IContractMilestoneQueries milestoneQueries
    ) : IUpdateHandler<ContractMilestone, UpdateContractMilestoneVM>
{
    public async Task<Result<ContractMilestone, ServiceResponse>> HandleAsync(ContractMilestone existingEntity,
        ContractMilestone? mappedEntity,
        UpdateContractMilestoneVM updateModel, CancellationToken cancellationToken)
    {
        // Перевірка чи змінився amount
        if (existingEntity.Amount == updateModel.Amount)
        {
            return Result<ContractMilestone, ServiceResponse>.Success(null); // Якщо amount не змінився, валідація не потрібна і змінна сутності теж
        }

        // Отримай контракт
        var contract = await contractQueries.GetByIdAsync(
            existingEntity.ContractId, 
            cancellationToken);
            
        if (contract == null)
        {
            return Result<ContractMilestone, ServiceResponse>.Failure(ServiceResponse.NotFound(
                $"Contract with ID {existingEntity.ContractId} not found"));
        }

        // Отримай всі milestone для контракту
        var allMilestones = await milestoneQueries.GetByContractIdAsync(
            existingEntity.ContractId, 
            cancellationToken);

        // Порахуй загальну суму (виключаючи поточний milestone)
        var totalAmount = allMilestones
            .Where(m => m.Id != existingEntity.Id)
            .Sum(m => m.Amount) + updateModel.Amount;

        // Перевір чи не перевищує бюджет
        if (totalAmount > contract.AgreedRate)
        {
            return Result<ContractMilestone, ServiceResponse>.Failure(ServiceResponse.BadRequest(
                $"The total amount ({totalAmount}) of milestones exceeds " +
                $"the contract's agreed rate ({contract.AgreedRate})"));
        }

        return Result<ContractMilestone, ServiceResponse>.Success(null);  // Валідація пройшла успішно
    }
}