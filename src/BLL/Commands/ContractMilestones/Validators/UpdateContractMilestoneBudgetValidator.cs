using BLL.Common.Interfaces.Repositories.ContractMilestones;
using BLL.Common.Interfaces.Repositories.Contracts;
using BLL.Common.Validators;
using BLL.Services;
using BLL.ViewModels.ContractMilestone;
using Domain.Models.Freelance;

namespace BLL.Commands.ContractMilestones.Validators;

public class UpdateContractMilestoneBudgetValidator(
    IContractQueries contractQueries,
    IContractMilestoneQueries milestoneQueries) 
    : IUpdateValidator<ContractMilestone, UpdateContractMilestoneVM>
{
    public async Task<ServiceResponse?> ValidateAsync(
        ContractMilestone existingMilestone,
        UpdateContractMilestoneVM updateModel,
        CancellationToken cancellationToken)
    {
        // Перевірка чи змінився amount
        if (existingMilestone.Amount == updateModel.Amount)
        {
            return null; // Якщо amount не змінився, валідація не потрібна
        }

        // Отримай контракт
        var contract = await contractQueries.GetByIdAsync(
            existingMilestone.ContractId, 
            cancellationToken);
            
        if (contract == null)
        {
            return ServiceResponse.NotFound(
                $"Contract with ID {existingMilestone.ContractId} not found");
        }

        // Отримай всі milestone для контракту
        var allMilestones = await milestoneQueries.GetByContractIdAsync(
            existingMilestone.ContractId, 
            cancellationToken);

        // Порахуй загальну суму (виключаючи поточний milestone)
        var totalAmount = allMilestones
            .Where(m => m.Id != existingMilestone.Id)
            .Sum(m => m.Amount) + updateModel.Amount;

        // Перевір чи не перевищує бюджет
        if (totalAmount > contract.AgreedRate)
        {
            return ServiceResponse.BadRequest(
                $"The total amount ({totalAmount}) of milestones exceeds " +
                $"the contract's agreed rate ({contract.AgreedRate})");
        }

        return null; // Валідація пройшла успішно
    }
}