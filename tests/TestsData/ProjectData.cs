using Domain.Models.Projects;

namespace TestsData;

public class ProjectData
{
    public static Project CreateProject(Guid? id = null, Guid? userId = null)
    {
        return new Project
        {
            Id = id ?? Guid.NewGuid(),
            Title = "Test Project",
            Description = "Test Project Description",
            BudgetMin = 1000m,
            BudgetMax = 5000m,
            IsHourly = false,
            Status = ProjectStatus.Open,
            CreatedBy = userId ?? Guid.NewGuid()
        };
    }
}
