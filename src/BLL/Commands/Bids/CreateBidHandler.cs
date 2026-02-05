using BLL.Common.Handlers;
using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Freelancers;
using BLL.Common.Interfaces.Repositories.Projects;
using BLL.Services;
using BLL.ViewModels.Bid;
using Domain.Models.Projects;

namespace BLL.Commands.Bids;

/// <summary>
/// Unified handler for Bid creation that combines validation and processing.
/// Replaces CreateBidValidator + CreateBidProcessor.
/// </summary>
public class CreateBidHandler(
    IProjectQueries projectQueries,
    IUserProvider userProvider,
    IFreelancerQueries freelancerQueries)
    : ICreateHandler<Bid, CreateBidVM>
{
    public async Task<Result<Bid, ServiceResponse>> HandleAsync(
        Bid entity,
        CreateBidVM createModel,
        CancellationToken cancellationToken)
    {
        // Validation: Check if project exists
        var existingProject = await projectQueries.GetByIdAsync(createModel.ProjectId, cancellationToken);
        
        if (existingProject is null)
        {
            return Result<Bid, ServiceResponse>.Failure(
                ServiceResponse.NotFound($"Project with Id {createModel.ProjectId} not found"));
        }

        // Processing: Set FreelancerId from current user
        var userId = await userProvider.GetUserId();
        var existingFreelancer = await freelancerQueries.GetByUserIdAsync(userId, cancellationToken);
        
        if (existingFreelancer is null)
        {
            return Result<Bid, ServiceResponse>.Failure(
                ServiceResponse.NotFound("Freelancer not found for current user"));
        }
        
        entity.FreelancerId = existingFreelancer.Id;

        // Return success with processed entity
        return Result<Bid, ServiceResponse>.Success(entity);
    }
}
