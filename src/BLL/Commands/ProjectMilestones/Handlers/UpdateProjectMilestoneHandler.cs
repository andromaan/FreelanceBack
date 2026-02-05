using AutoMapper;
using BLL.Common.Handlers;
using BLL.Common.Interfaces.Repositories.ProjectMilestones;
using BLL.Common.Interfaces.Repositories.Projects;
using BLL.Services;
using BLL.ViewModels.ProjectMilestone;
using Domain.Models.Projects;

namespace BLL.Commands.ProjectMilestones.Handlers;

public class UpdateProjectMilestoneHandler(
    IProjectQueries projectQueries,
    IProjectMilestoneQueries milestoneQueries,
    IMapper mapper
    ) : IUpdateHandler<ProjectMilestone, UpdateProjectMilestoneVM>
{
    public async Task<ServiceResponse?> HandleAsync(
        ProjectMilestone existingEntity,
        UpdateProjectMilestoneVM updateModel, CancellationToken cancellationToken)
    {
        // Перевірка чи змінився amount
        if (existingEntity.Amount == updateModel.Amount)
        {
            return ServiceResponse.Ok(); // Якщо amount не змінився, повертаємо null - це означає використовувати entity після маппінгу в Update.cs
        }

        // Отримай проєкт
        var project = await projectQueries.GetByIdAsync(
            existingEntity.ProjectId, 
            cancellationToken,
            asNoTracking: true);
            
        if (project == null)
        {
            return ServiceResponse.NotFound(
                $"Project with ID {existingEntity.ProjectId} not found");
        }

        // Отримай всі milestone для проєкту
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
            return ServiceResponse.BadRequest(
                $"The total amount ({totalAmount}) of milestones exceeds " +
                $"the project's budged ({project.Budget})");
        }
        
        mapper.Map(updateModel, existingEntity);

        // Валідація пройшла успішно, повертаємо null щоб використати entity після маппінгу в Update.cs
        return ServiceResponse.Ok();
    }
}