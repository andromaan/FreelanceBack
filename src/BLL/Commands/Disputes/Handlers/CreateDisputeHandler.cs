using BLL.Common.Handlers;
using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Contracts;
using BLL.Services;
using BLL.ViewModels.Dispute;
using Domain;
using Domain.Models.Disputes;
using BLL.Common.Interfaces.Repositories.Freelancers;
using Domain.Models.Contracts;

namespace BLL.Commands.Disputes.Handlers;

public class CreateDisputeHandler(
    IUserProvider userProvider,
    IContractQueries contractQueries,
    IFreelancerQueries freelancerQueries,
    IContractRepository contractRepository
) : ICreateHandler<Dispute, CreateDisputeVM>
{
    public async Task<ServiceResponse?> HandleAsync(Dispute entity, CreateDisputeVM createModel,
        CancellationToken cancellationToken)
    {
        var userRole = userProvider.GetUserRole();
        var userId = await userProvider.GetUserId();

        var existingContract = await contractQueries.GetByIdAsync(createModel.ContractId, cancellationToken);

        if (existingContract is null)
        {
            return ServiceResponse.NotFound($"Contract with Id {createModel.ContractId} not found");
        }

        var freelancer = await freelancerQueries.GetByUserIdAsync(userId, cancellationToken);

        var isCreator = existingContract.CreatedBy == userId;
        var isAdminOrModerator = userRole == Settings.Roles.AdminRole || userRole == Settings.Roles.ModeratorRole;
        var isFreelancer = existingContract.FreelancerId == freelancer?.Id;
        if (!isCreator && !isAdminOrModerator && !isFreelancer)
        {
            return ServiceResponse.Unauthorized("You are not authorized to create a dispute for this contract");
        }
        
        if (existingContract is {Status: ContractStatus.Pending})
        {
            return ServiceResponse.BadRequest("Cannot create a dispute for a pending contract");
        }
        
        if (existingContract is {Status: ContractStatus.Disputed})
        {
            return ServiceResponse.BadRequest("Cannot create a dispute for a contract that is already disputed");
        }
        
        if (existingContract is {Status: ContractStatus.Completed})
        {
            return ServiceResponse.BadRequest("Cannot create a dispute for a completed contract");
        }
        
        if (existingContract is {Status: ContractStatus.Cancelled})
        {
            return ServiceResponse.BadRequest("Cannot create a dispute for a cancelled contract");
        }

        existingContract.Status = ContractStatus.Disputed;

        try
        {
            await contractRepository.UpdateAsync(existingContract, cancellationToken);
        }
        catch (Exception e)
        {
            return ServiceResponse.InternalError("An error occurred while creating the dispute", e);
        }

        return ServiceResponse.Ok();
    }
}