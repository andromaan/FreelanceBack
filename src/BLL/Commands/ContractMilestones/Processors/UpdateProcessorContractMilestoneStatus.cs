using BLL.Common.Interfaces.Repositories.Contracts;
using BLL.Common.Interfaces.Repositories.Freelancers;
using BLL.Common.Interfaces.Repositories.UserWallets;
using BLL.Common.Interfaces.Repositories.WalletTransactions;
using BLL.Common.Processors;
using BLL.ViewModels.ContractMilestone;
using Domain.Models.Auth.Users;
using Domain.Models.Freelance;

namespace BLL.Commands.ContractMilestones.Processors;

public class UpdateProcessorMessage(
    IUserWalletRepository userWalletRepository,
    IWalletTransactionRepository walletTransactionRepository,
    IContractQueries contractQueries,
    IFreelancerQueries freelancerQueries)
    : IUpdateProcessor<ContractMilestone, UpdContractMilestoneStatusEmployerVM>
{
    public async Task<ContractMilestone> ProcessAsync(ContractMilestone entity,
        UpdContractMilestoneStatusEmployerVM updateVm, CancellationToken cancellationToken)
    {
        if (updateVm.Status == ContractMilestoneEmployerStatus.Approved)
        {
            await TransactionLogic(entity, cancellationToken);
        }
        
        entity.Status = (ContractMilestoneStatus)updateVm.Status;
        return entity;
    }
    
    private async Task TransactionLogic(ContractMilestone entity, CancellationToken cancellationToken)
    {
        var contract = await contractQueries.GetByIdAsync(entity.ContractId, cancellationToken);
        var freelancer = await freelancerQueries.GetByIdAsync(contract!.FreelancerId, cancellationToken);
        
        var employerWallet = await userWalletRepository.WithdrawAsync(entity.CreatedBy, entity.Amount, cancellationToken);
        
        var freelancerWallet = await userWalletRepository.DepositAsync(freelancer!.CreatedBy, entity.Amount, cancellationToken);

        await walletTransactionRepository.CreateAsync(new WalletTransaction
        {
            Id = Guid.NewGuid(),
            Amount = -entity.Amount,
            TransactionDate = DateTime.UtcNow,
            TransactionType = "Payment for milestone",
            WalletId =  employerWallet!.Id,
        }, cancellationToken);
        
        await walletTransactionRepository.CreateAsync(new WalletTransaction
        {
            Id = Guid.NewGuid(),
            Amount = entity.Amount,
            TransactionDate = DateTime.UtcNow,
            TransactionType = "Received payment for milestone",
            WalletId =  freelancerWallet!.Id,
        }, cancellationToken);
    }
}