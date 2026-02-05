using BLL.Common.Handlers;
using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Freelancers;
using BLL.Common.Interfaces.Repositories.Projects;
using BLL.Services;
using BLL.ViewModels.Quote;
using Domain.Models.Projects;

namespace BLL.Commands.Quotes;

/// <summary>
/// Unified handler for Quote creation that combines validation and processing.
/// Replaces CreateQuoteValidator + CreateQuoteProcessor.
/// </summary>
public class CreateQuoteHandler(
    IProjectQueries projectQueries,
    IUserProvider userProvider,
    IFreelancerQueries freelancerQueries)
    : ICreateHandler<Quote, CreateQuoteVM>
{
    public async Task<Result<Quote, ServiceResponse>> HandleAsync(
        Quote entity,
        CreateQuoteVM createModel,
        CancellationToken cancellationToken)
    {
        // Validation: Check if project exists
        var existingProject = await projectQueries.GetByIdAsync(createModel.ProjectId, cancellationToken);
        
        if (existingProject is null)
        {
            return Result<Quote, ServiceResponse>.Failure(
                ServiceResponse.NotFound($"Project with Id {createModel.ProjectId} not found"));
        }

        // Processing: Set FreelancerId from current user
        var userId = await userProvider.GetUserId();
        var existingFreelancer = await freelancerQueries.GetByUserIdAsync(userId, cancellationToken);
        
        if (existingFreelancer is null)
        {
            return Result<Quote, ServiceResponse>.Failure(
                ServiceResponse.NotFound("Freelancer not found for current user"));
        }
        
        entity.FreelancerId = existingFreelancer.Id;

        // Return success with processed entity
        return Result<Quote, ServiceResponse>.Success(entity);
    }
}
