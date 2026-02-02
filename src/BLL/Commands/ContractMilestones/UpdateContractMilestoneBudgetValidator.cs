using BLL.Common.Interfaces.Repositories.ContractMilestones;
using BLL.Common.Interfaces.Repositories.Contracts;
using BLL.Common.Validators;
using BLL.Services;
using BLL.ViewModels.ContractMilestone;
using Domain.Models.Freelance;

namespace BLL.Commands.ContractMilestones;

public class UpdateContractMilestoneBudgetValidator(
    IContractQueries contractQueries,
    IContractMilestoneQueries milestoneQueries) 
    : IUpdateValidator<ContractMilestone, UpdateContractMilestoneVM>
{
    public async Task<ServiceResponse?> ValidateAsync(
        ContractMilestone existingEmployer,
        UpdateContractMilestoneVM updateModel,
        CancellationToken cancellationToken)
    {
        // Перевірка чи змінився amount
        if (existingEmployer.Amount == updateModel.Amount)
        {
            return null; // Якщо amount не змінився, валідація не потрібна
        }

        // Отримай контракт
        var contract = await contractQueries.GetByIdAsync(
            existingEmployer.ContractId, 
            cancellationToken);
            
        if (contract == null)
        {
            return ServiceResponse.NotFound(
                $"Contract with ID {existingEmployer.ContractId} not found");
        }

        // Отримай всі milestone для контракту
        var allMilestones = await milestoneQueries.GetByContractIdAsync(
            existingEmployer.ContractId, 
            cancellationToken);

        // Порахуй загальну суму (виключаючи поточний milestone)
        var totalAmount = allMilestones
            .Where(m => m.Id != existingEmployer.Id)
            .Sum(m => m.Amount) + updateModel.Amount;

        // Перевір чи не перевищує бюджет
        if (totalAmount > contract.AgreedRate)
        {
            return ServiceResponse.BadRequest(
                $"The total amount ({totalAmount}) of milestones exceeds " +
                $"the contract's maximum budget ({contract.AgreedRate})");
        }

        return null; // Валідація пройшла успішно
    }
}