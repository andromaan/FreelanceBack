using BLL.Common.Handlers;
using BLL.Common.Interfaces.Repositories.ProjectMilestones;
using BLL.Common.Interfaces.Repositories.Projects;
using BLL.Services;
using BLL.ViewModels.ProjectMilestone;
using Domain.Models.Projects;

namespace BLL.Commands.ProjectMilestones.Handlers;

public class UpdateProjectMilestoneHandler(
    IProjectQueries projectQueries,
    IProjectMilestoneQueries milestoneQueries
    ) : IUpdateHandler<ProjectMilestone, UpdateProjectMilestoneVM>
{
    public async Task<Result<ProjectMilestone, ServiceResponse>> HandleAsync(ProjectMilestone existingEntity,
        ProjectMilestone? mappedEntity,
        UpdateProjectMilestoneVM updateModel, CancellationToken cancellationToken)
    {
        // Перевірка чи змінився amount
        if (existingEntity.Amount == updateModel.Amount)
        {
            return Result<ProjectMilestone, ServiceResponse>.Success(null); // Якщо amount не змінився, валідація не потрібна і змінна сутності теж
        }

        // Отримай контракт
        var project = await projectQueries.GetByIdAsync(
            existingEntity.ProjectId, 
            cancellationToken);
            
        if (project == null)
        {
            return Result<ProjectMilestone, ServiceResponse>.Failure(ServiceResponse.NotFound(
                $"Project with ID {existingEntity.ProjectId} not found"));
        }

        // Отримай всі milestone для контракту
        var allMilestones = await milestoneQueries.GetByProjectIdAsync(
            existingEntity.ProjectId, 
            cancellationToken);

        // Порахуй загальну суму (виключаючи поточний milestone)
        var totalAmount = allMilestones
            .Where(m => m.Id != existingEntity.Id)
            .Sum(m => m.Amount) + updateModel.Amount;

        // Перевір чи не перевищує бюджет
        if (totalAmount > project.Budget)
        {
            return Result<ProjectMilestone, ServiceResponse>.Failure(ServiceResponse.BadRequest(
                $"The total amount ({totalAmount}) of milestones exceeds " +
                $"the project's budged ({project.Budget})"));
        }

        return Result<ProjectMilestone, ServiceResponse>.Success(null);  // Валідація пройшла успішно
    }
}