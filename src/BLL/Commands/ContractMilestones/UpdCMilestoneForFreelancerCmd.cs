using AutoMapper;
using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.ContractMilestones;
using BLL.Common.Interfaces.Repositories.Contracts;
using BLL.Common.Interfaces.Repositories.Freelancers;
using BLL.Services;
using BLL.ViewModels.ContractMilestone;
using MediatR;

namespace BLL.Commands.ContractMilestones;

public record UpdCMilestoneForFreelancerCmd : IRequest<ServiceResponse>
{
    public required Guid Id { get; init; }
    public required ContractMilestoneFreelancerStatus Status { get; init; }
}

public class UpdCMilestoneForFreelancerCmdHandler(
    IContractMilestoneRepository contractMilestoneRepository,
    IContractMilestoneQueries contractMilestoneQueries,
    IMapper mapper,
    IUserProvider userProvider,
    IContractQueries contractQueries,
    IFreelancerQueries freelancerQueries)
    : IRequestHandler<UpdCMilestoneForFreelancerCmd, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(UpdCMilestoneForFreelancerCmd request,
        CancellationToken cancellationToken)
    {
        // Перевірка існування entity
        var existingContractMilestone = await contractMilestoneQueries.GetByIdAsync(request.Id, cancellationToken);

        if (existingContractMilestone == null)
        {
            return ServiceResponse.NotFound(
                $"Contract milestone with ID {request.Id} not found");
        }
        
        // Перевірка прав доступу
        var userId = await userProvider.GetUserId();
        var contract = await contractQueries.GetByIdAsync(existingContractMilestone.ContractId, cancellationToken);
        var freelancer = await freelancerQueries.GetByUserIdAsync(userId, cancellationToken);

        if (contract!.FreelancerId != freelancer!.Id)
        {
            return ServiceResponse.Forbidden("You do not have permission to edit this entity");
        }
        
        existingContractMilestone.Status = (Domain.Models.Freelance.ContractMilestoneStatus)request.Status;
        var contractMilestoneToUpdate = existingContractMilestone;

        // Збереження
        try
        {
            await contractMilestoneRepository.UpdateAsync(contractMilestoneToUpdate, cancellationToken);
            return ServiceResponse.Ok(
                $"Status for contract milestone with ID {request.Id} updated successfully",
                mapper.Map<ContractMilestoneVM>(contractMilestoneToUpdate));
        }
        catch (Exception exception)
        {
            return ServiceResponse.InternalError(exception.Message);
        }
    }
}