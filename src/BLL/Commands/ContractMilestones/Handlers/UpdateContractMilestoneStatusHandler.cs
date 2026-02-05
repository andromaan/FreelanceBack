using System.Net;
using BLL.Common.Handlers;
using BLL.Common.Interfaces.Repositories.Contracts;
using BLL.Common.Interfaces.Repositories.Freelancers;
using BLL.Common.Interfaces.Repositories.UserWallets;
using BLL.Common.Interfaces.Repositories.WalletTransactions;
using BLL.Services;
using BLL.ViewModels.ContractMilestone;
using Domain.Models.Auth.Users;
using Domain.Models.Freelance;

namespace BLL.Commands.ContractMilestones.Handlers;

/// <summary>
/// Unified handler for ContractMilestone status update that combines validation and processing.
/// Replaces UpdateContractMilestoneStatusValidator.
/// </summary>
public class UpdateContractMilestoneStatusHandler(
    IUserWalletRepository userWalletRepository,
    IWalletTransactionRepository walletTransactionRepository,
    IContractQueries contractQueries,
    IFreelancerQueries freelancerQueries
)
    : IUpdateHandler<ContractMilestone, UpdContractMilestoneStatusEmployerVM>
{
    public async Task<Result<ContractMilestone, ServiceResponse>> HandleAsync(
        ContractMilestone existingEntity,
        ContractMilestone mappedEntity,
        UpdContractMilestoneStatusEmployerVM updateModel,
        CancellationToken cancellationToken)
    {
        // Validation: Check if milestone is already approved
        if (existingEntity.Status == ContractMilestoneStatus.Approved)
        {
            return Result<ContractMilestone, ServiceResponse>.Failure(
                ServiceResponse.GetResponse(
                    "Cannot change the status of an approved contract milestone.",
                    false, null, HttpStatusCode.BadRequest));
        }

        // Validation: Check wallet balance if approving
        if (updateModel.Status == ContractMilestoneEmployerStatus.Approved)
        {
            var contract = await contractQueries.GetByIdAsync(existingEntity.ContractId, cancellationToken);
            var freelancer = await freelancerQueries.GetByIdAsync(contract!.FreelancerId, cancellationToken);

            var employerWallet = await userWalletRepository.WithdrawAsync(existingEntity.CreatedBy,
                existingEntity.Amount, cancellationToken);

            if (employerWallet is null)
            {
                return Result<ContractMilestone, ServiceResponse>.Failure(
                    ServiceResponse.GetResponse(
                        "Insufficient funds in the employer's wallet to approve this milestone.",
                        false, null, HttpStatusCode.BadRequest));
            }

            var freelancerWallet =
                await userWalletRepository.DepositAsync(freelancer!.CreatedBy, existingEntity.Amount,
                    cancellationToken);
            
            await walletTransactionRepository.CreateAsync(new WalletTransaction
            {
                Id = Guid.NewGuid(),
                Amount = -existingEntity.Amount,
                TransactionDate = DateTime.UtcNow,
                TransactionType = "Payment for milestone",
                WalletId =  employerWallet.Id,
            }, cancellationToken);
        
            await walletTransactionRepository.CreateAsync(new WalletTransaction
            {
                Id = Guid.NewGuid(),
                Amount = existingEntity.Amount,
                TransactionDate = DateTime.UtcNow,
                TransactionType = "Received payment for milestone",
                WalletId =  freelancerWallet!.Id,
            }, cancellationToken);
        }

        mappedEntity.Status = (ContractMilestoneStatus)updateModel.Status;
        
        // Processing: No additional processing needed, return mapped entity
        return Result<ContractMilestone, ServiceResponse>.Success(mappedEntity);
    }
}