using System.Net;
using AutoMapper;
using BLL.Common.Handlers;
using BLL.Common.Interfaces.Repositories.ContractMilestones;
using BLL.Common.Interfaces.Repositories.ContractPayments;
using BLL.Common.Interfaces.Repositories.Contracts;
using BLL.Common.Interfaces.Repositories.Freelancers;
using BLL.Common.Interfaces.Repositories.Projects;
using BLL.Common.Interfaces.Repositories.UserWallets;
using BLL.Common.Interfaces.Repositories.WalletTransactions;
using BLL.Services;
using BLL.ViewModels.ContractMilestone;
using Domain.Models.Contracts;
using Domain.Models.Payments;
using Domain.Models.Projects;

namespace BLL.Commands.ContractMilestones.Handlers;

public class UpdateContractMilestoneStatusModeratorHandler(
    IUserWalletRepository userWalletRepository,
    IWalletTransactionRepository walletTransactionRepository,
    IContractQueries contractQueries,
    IContractRepository contractRepository,
    IFreelancerQueries freelancerQueries,
    IContractMilestoneQueries contractMilestoneQueries,
    IMapper mapper,
    IContractPaymentRepository contractPaymentRepository,
    IProjectRepository projectRepository,
    IProjectQueries projectQueries
)
    : IUpdateHandler<ContractMilestone, UpdContractMilestoneStatusModeratorVM>
{
    public async Task<ServiceResponse?> HandleAsync(
        ContractMilestone existingEntity,
        UpdContractMilestoneStatusModeratorVM updateModel,
        CancellationToken cancellationToken)
    {
        // Validation: Check if milestone is already approved
        if (existingEntity.Status == ContractMilestoneStatus.Approved)
        {
            return ServiceResponse.GetResponse(
                "Cannot change the status of an approved contract milestone.",
                false, null, HttpStatusCode.BadRequest);
        }
        
        var contract = await contractQueries.GetByIdAsync(existingEntity.ContractId, cancellationToken);

        // Validation: Check wallet balance if approving
        if (updateModel.Status == ContractMilestoneStatus.Approved)
        {
            var paymentResult = await ProcessPayment(existingEntity, contract, cancellationToken);
            if (paymentResult != null)
                return paymentResult;
        }

        // Processing: Update contract status if all milestones are approved or rejected
        var contractStatusChangeResult =
            await UpdateContractStatusIfNeeded(existingEntity, contract, updateModel, cancellationToken);
        if (contractStatusChangeResult != null)
            return contractStatusChangeResult;


        mapper.Map(updateModel, existingEntity);

        // Processing: No additional processing needed, return mapped entity
        return ServiceResponse.Ok();
    }

    private async Task<ServiceResponse?> UpdateContractStatusIfNeeded(ContractMilestone existingEntity,
        Contract? contract, UpdContractMilestoneStatusModeratorVM updateModel, CancellationToken cancellationToken)
    {
        var contractMilestonesByContract =
            (await contractMilestoneQueries.GetByContractIdAsync(existingEntity.ContractId, cancellationToken))
            .ToList();

        var isAllMilestonesCompleted = contractMilestonesByContract.Where(m => m.Id != existingEntity.Id)
            .All(m => m is { Status: ContractMilestoneStatus.Approved or ContractMilestoneStatus.Rejected });
        var isUpdatedModelCompleted = updateModel is
            { Status: ContractMilestoneStatus.Approved };

        if (isAllMilestonesCompleted && isUpdatedModelCompleted)
        {
            contract!.Status = ContractStatus.Completed;
            
            var project = await projectQueries.GetByIdAsync(contract.ProjectId, cancellationToken);
            project!.Status = ProjectStatus.Completed;
            
            try
            {
                await projectRepository.UpdateAsync(project, cancellationToken);
                await contractRepository.UpdateAsync(contract, cancellationToken);
            }
            catch (Exception e)
            {
                return ServiceResponse.InternalError(e.Message, e.InnerException);
            }
        }

        return null;
    }

    private async Task<ServiceResponse?> ProcessPayment(ContractMilestone existingEntity,
        Contract? contract,
        CancellationToken cancellationToken)
    {
        var freelancer = await freelancerQueries.GetByIdAsync(contract!.FreelancerId, cancellationToken);

        var employerWallet = await userWalletRepository.WithdrawAsync(existingEntity.CreatedBy,
            existingEntity.Amount, cancellationToken);

        if (employerWallet is null)
        {
            return ServiceResponse.GetResponse(
                "Insufficient funds in the employer's wallet to approve this milestone.",
                false, null, HttpStatusCode.BadRequest);
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
            WalletId = employerWallet.Id,
        }, cancellationToken);

        await walletTransactionRepository.CreateAsync(new WalletTransaction
        {
            Id = Guid.NewGuid(),
            Amount = existingEntity.Amount,
            TransactionDate = DateTime.UtcNow,
            TransactionType = "Received payment for milestone",
            WalletId = freelancerWallet!.Id,
        }, cancellationToken);

        await contractPaymentRepository.CreateAsync(new ContractPayment
        {
            Id = Guid.NewGuid(),
            ContractId = contract.Id,
            MilestoneId = existingEntity.Id,
            Amount = existingEntity.Amount,
            PaymentDate = DateTime.UtcNow,
            PaymentMethod = "Wallet"
        }, cancellationToken);

        return null;
    }
}