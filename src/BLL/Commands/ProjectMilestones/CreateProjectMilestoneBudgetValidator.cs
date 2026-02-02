using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.ProjectMilestones;
using BLL.Common.Interfaces.Repositories.Projects;
using BLL.Common.Validators;
using BLL.Services;
using BLL.ViewModels.ProjectMilestone;
using Domain;

namespace BLL.Commands.ProjectMilestones;

public class CreateProjectMilestoneBudgetValidator(
    IProjectQueries projectQueries,
    IProjectMilestoneQueries milestoneQueries,
    IUserProvider userProvider) 
    : ICreateValidator<CreateProjectMilestoneVM>
{
    public async Task<ServiceResponse?> ValidateAsync(
        CreateProjectMilestoneVM createModel,
        CancellationToken cancellationToken)
    {
        var userRole = userProvider.GetUserRole();
        var userId = await userProvider.GetUserId();
        
        var existingProject = await projectQueries.GetByIdAsync(createModel.ProjectId, cancellationToken);

        if (existingProject is null)
        {
            return ServiceResponse.NotFound($"Project with Id {createModel.ProjectId} not found");
        }

        if (existingProject.CreatedBy != userId && userRole != Settings.Roles.AdminRole)
        {
            return ServiceResponse.Unauthorized("You are not authorized to create a milestone for this project");
        }

        var existingMilestones =
            await milestoneQueries.GetByProjectIdAsync(createModel.ProjectId, cancellationToken);

        var totalMilestoneAmount = existingMilestones.Sum(x => x.Amount) + createModel.Amount;
        if (totalMilestoneAmount > existingProject.BudgetMax)
        {
            return ServiceResponse.GetResponse(
                $"The total amount ({totalMilestoneAmount}) of milestones exceeds " +
                $"the project's maximum budget ({existingProject.BudgetMax})",
                false, null, System.Net.HttpStatusCode.BadRequest);
        }

        return null; // Валідація пройшла успішно
    }
}