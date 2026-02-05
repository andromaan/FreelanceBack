using BLL.Common.Interfaces.Repositories.ProjectMilestones;
using BLL.Common.Interfaces.Repositories.Projects;
using BLL.Common.Validators;
using BLL.Services;
using BLL.ViewModels.ProjectMilestone;
using Domain.Models.Projects;

namespace BLL.Commands.ProjectMilestones;

public class UpdateProjectMilestoneBudgetValidator(
    IProjectQueries projectQueries,
    IProjectMilestoneQueries milestoneQueries) 
    : IUpdateValidator<ProjectMilestone, UpdateProjectMilestoneVM>
{
    public async Task<ServiceResponse?> ValidateAsync(
        ProjectMilestone existingMilestone,
        UpdateProjectMilestoneVM updateModel,
        CancellationToken cancellationToken)
    {
        // Перевірка чи змінився amount
        if (existingMilestone.Amount == updateModel.Amount)
        {
            return null; // Якщо amount не змінився, валідація не потрібна
        }

        // Отримай проект
        var project = await projectQueries.GetByIdAsync(
            existingMilestone.ProjectId, 
            cancellationToken);
            
        if (project == null)
        {
            return ServiceResponse.NotFound(
                $"Project with ID {existingMilestone.ProjectId} not found");
        }

        // Отримай всі milestone для проекту
        var allMilestones = await milestoneQueries.GetByProjectIdAsync(
            existingMilestone.ProjectId, 
            cancellationToken);

        // Порахуй загальну суму (виключаючи поточний milestone)
        var totalAmount = allMilestones
            .Where(m => m.Id != existingMilestone.Id)
            .Sum(m => m.Amount) + updateModel.Amount;

        // Перевір чи не перевищує бюджет
        if (totalAmount > project.Budget)
        {
            return ServiceResponse.BadRequest(
                $"The total amount ({totalAmount}) of milestones exceeds " +
                $"the project's maximum budget ({project.Budget})");
        }

        return null; // Валідація пройшла успішно
    }
}