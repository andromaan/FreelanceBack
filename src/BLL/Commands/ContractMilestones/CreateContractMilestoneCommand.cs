using AutoMapper;
using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.ContractMilestones;
using BLL.Common.Interfaces.Repositories.Contracts;
using BLL.Services;
using BLL.ViewModels.ContractMilestone;
using Domain;
using Domain.Models.Freelance;
using MediatR;

namespace BLL.Commands.ContractMilestones;

public record CreateContractMilestoneCommand(CreateContractMilestoneVM CreateVm) : IRequest<ServiceResponse>;

public class CreateContractMilestoneCommandHandler(
    IContractQueries contractQueries,
    IMapper mapper,
    IContractMilestoneRepository contractMilestoneRepository,
    IUserProvider userProvider)
    : IRequestHandler<CreateContractMilestoneCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(CreateContractMilestoneCommand request, CancellationToken cancellationToken)
    {
        var createVm = request.CreateVm;
        
        var userRole = userProvider.GetUserRole();
        var userId = await userProvider.GetUserId();
        
        var existingContract = await contractQueries.GetByIdAsync(createVm.ContractId, cancellationToken);
        
        if (existingContract is null)
        {
            return ServiceResponse.NotFound($"Contract with Id {createVm.ContractId} not found");
        }
        
        if (existingContract.CreatedBy != userId && userRole != Settings.Roles.AdminRole)
        {
            return ServiceResponse.Unauthorized("You are not authorized to create a milestone for this contract");
        }

        var contractMilestone = mapper.Map<ContractMilestone>(createVm);
        contractMilestone.Status = ContractMilestoneStatus.Pending;

        try
        {
            var createContractMilestone = await contractMilestoneRepository.CreateAsync(contractMilestone, cancellationToken);
            return ServiceResponse.Ok($"Contract milestone created",
                mapper.Map<ContractMilestoneVM>(createContractMilestone));
        }
        catch (Exception exception)
        {
            return ServiceResponse.InternalError(exception.Message, data: exception.InnerException?.Message);
        }
    }
}