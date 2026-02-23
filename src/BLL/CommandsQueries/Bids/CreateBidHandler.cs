using BLL.Common.Handlers;
using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Employers;
using BLL.Common.Interfaces.Repositories.Freelancers;
using BLL.Common.Interfaces.Repositories.Projects;
using BLL.Services;
using BLL.Services.Notifications;
using BLL.ViewModels.Bid;
using Domain.Models.Notifications;
using Domain.Models.Projects;

namespace BLL.CommandsQueries.Bids;

/// <summary>
/// Unified handler for Bid creation that combines validation and processing.
/// Replaces CreateBidValidator + CreateBidProcessor.
/// </summary>
public class CreateBidHandler(
    IProjectQueries projectQueries,
    IUserProvider userProvider,
    IFreelancerQueries freelancerQueries,
    IEmployerQueries employerQueries,
    INotificationService notificationService)
    : ICreateHandler<Bid, CreateBidVM>
{
    public async Task<ServiceResponse?> HandleAsync(
        Bid entity,
        CreateBidVM createModel,
        CancellationToken cancellationToken)
    {
        // Validation: Check if project exists
        var existingProject = await projectQueries.GetByIdAsync(createModel.ProjectId, cancellationToken);

        if (existingProject is null)
        {
            return ServiceResponse.NotFound($"Project with Id {createModel.ProjectId} not found");
        }

        // Processing: Set FreelancerId from current user
        var userId = await userProvider.GetUserId();
        var existingFreelancer = await freelancerQueries.GetByUserIdAsync(userId, cancellationToken);

        if (existingFreelancer is null)
        {
            return ServiceResponse.NotFound("Freelancer not found for current user");
        }

        if (existingProject.Budget < createModel.Amount)
        {
            return ServiceResponse.BadRequest(
                $"Bid amount {createModel.Amount} exceeds project budget {existingProject.Budget}");
        }

        entity.FreelancerId = existingFreelancer.Id;

        // Notify: Find employer (owner of the project) and send notification
        var employer = await employerQueries.GetByUserId(existingProject.CreatedBy, cancellationToken);
        if (employer is not null)
        {
            await notificationService.SendAsync(
                message: $"You received a new bid of {createModel.Amount:C} on your project \"{existingProject.Title}\".",
                type: NotificationType.NewBidReceived,
                userId: existingProject.CreatedBy,
                cancellationToken: cancellationToken);
        }

        // Return success with processed entity
        return ServiceResponse.Ok();
    }
}

