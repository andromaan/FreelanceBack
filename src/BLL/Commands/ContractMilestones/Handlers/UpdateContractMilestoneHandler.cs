using AutoMapper;
using BLL.Common.Handlers;
using BLL.Common.Interfaces.Repositories.ContractMilestones;
using BLL.Common.Interfaces.Repositories.Contracts;
using BLL.Services;
using BLL.ViewModels.ContractMilestone;
using Domain.Models.Freelance;

namespace BLL.Commands.ContractMilestones.Handlers;

public class UpdateContractMilestoneHandler(
    IContractQueries contractQueries,
    IContractMilestoneQueries milestoneQueries,
    IMapper mapper
    ) : IUpdateHandler<ContractMilestone, UpdateContractMilestoneVM>
{
    public async Task<ServiceResponse?> HandleAsync(
        ContractMilestone existingEntity,
        UpdateContractMilestoneVM updateModel, CancellationToken cancellationToken)
    {
        // Перевірка чи змінився amount
        if (existingEntity.Amount == updateModel.Amount)
        {
            return ServiceResponse.Ok();// Якщо amount не змінився, валідація не потрібна і змінна сутності теж
        }

        // Отримай контракт
        var contract = await contractQueries.GetByIdAsync(
            existingEntity.ContractId, 
            cancellationToken, asNoTracking: true);
            
        if (contract == null)
        {
            return ServiceResponse.NotFound(
                $"Contract with ID {existingEntity.ContractId} not found");
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
            return ServiceResponse.BadRequest(
                $"The total amount ({totalAmount}) of milestones exceeds " +
                $"the contract's agreed rate ({contract.AgreedRate})");
        }
        
        mapper.Map(updateModel, existingEntity);

        return ServiceResponse.Ok();  // Валідація пройшла успішно
    }
}