using AutoMapper;
using BLL.Common.Interfaces.Repositories.ContractMilestones;
using BLL.Common.Interfaces.Repositories.Contracts;
using BLL.Common.Interfaces.Repositories.ProjectMilestones;
using BLL.Common.Interfaces.Repositories.Projects;
using BLL.Common.Interfaces.Repositories.Quotes;
using BLL.Services;
using BLL.ViewModels.Contract;
using Domain.Models.Freelance;
using Domain.Models.Projects;
using MediatR;

namespace BLL.Commands.Contracts;

public class CreateContractCommand : IRequest<ServiceResponse>
{
    public required Guid QuoteId { get; init; }
}

public class CreateContractCommandHandler(
    IContractRepository contractRepository,
    IQuoteQueries quoteQueries,
    IProjectQueries projectQueries,
    IMapper mapper,
    IProjectMilestoneQueries projectMilestoneQueries,
    IContractMilestoneRepository contractMilestoneRepository,
    IProjectRepository projectRepository,
    IProjectQueries projectQueries1)
    : IRequestHandler<CreateContractCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(CreateContractCommand request, CancellationToken cancellationToken)
    {
        var quote = await quoteQueries.GetByIdAsync(request.QuoteId, cancellationToken);
        if (quote is null)
        {
            return ServiceResponse.NotFound($"Quote with id {request.QuoteId} not found");
        }

        var project = await projectQueries.GetByIdAsync(quote.ProjectId, cancellationToken);
        var projectMilestones
            = (await projectMilestoneQueries.GetByProjectIdAsync(quote.ProjectId, cancellationToken)).ToList();

        var contract = new Contract
        {
            Id = Guid.NewGuid(),
            ProjectId = project!.Id,
            FreelancerId = quote.FreelancerId,
            StartDate = DateTime.UtcNow,
            EndDate = projectMilestones.Any()
                ? projectMilestones.OrderByDescending(x => x.DueDate).First().DueDate
                : project.Deadline,
            AgreedRate = quote.Amount,
            Status = ContractStatus.Pending,
        };

        try
        {
            var createdEntity = await contractRepository.CreateAsync(contract, cancellationToken);
            
            foreach (var pMilestone in projectMilestones)
            {
                var cMilestone = new ContractMilestone
                {
                    Id = Guid.NewGuid(),
                    ContractId = contract.Id,
                    Description = pMilestone.Description,
                    DueDate = pMilestone.DueDate,
                    Amount = pMilestone.Amount,
                    Status = ContractMilestoneStatus.Pending
                };
                
                await contractMilestoneRepository.CreateAsync(cMilestone, cancellationToken);
            }
            
            // Update project status to InProgress
            var projectChangeStatusResult = await UpdateStatusAsync(quote.ProjectId, cancellationToken);
            if (projectChangeStatusResult != null)
                return projectChangeStatusResult;
            
            return ServiceResponse.Ok($"Contract created",
                mapper.Map<ContractVM>(createdEntity));
        }
        catch (Exception exception)
        {
            return ServiceResponse.InternalError(exception.Message, data: exception.InnerException?.Message);
        }
    }

    private async Task<ServiceResponse?> UpdateStatusAsync(Guid quoteProjectId, CancellationToken cancellationToken)
    {
        var project = await projectQueries1.GetByIdAsync(quoteProjectId, cancellationToken);
        if (project is null)
        {
            return ServiceResponse.NotFound($"Project with id {quoteProjectId} not found");
        }
        
        project.Status = ProjectStatus.InProgress;
        
        try
        {
            await projectRepository.UpdateAsync(project, cancellationToken);
        }
        catch (Exception e)
        {
            return ServiceResponse.InternalError(e.Message, e.InnerException);
        }

        return null;
    }
}