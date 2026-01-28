using System.Net;
using System.Net.Http.Json;
using BLL.ViewModels.ProjectMilestone;
using Domain.Models.Auth.Users;
using Domain.Models.Projects;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Tests.Common;
using TestsData;

namespace Api.Tests.Integration;

public class ProjectMilestoneControllerTests(IntegrationTestWebFactory factory)
    : BaseIntegrationTest(factory), IAsyncLifetime
{
    private ProjectMilestone _projectMilestone = null!;
    private Project _project = null!;
    private User _employerUser = null!;

    [Fact]
    public async Task ShouldCreateProjectMilestone()
    {
        // Arrange
        var dueDate = DateTime.UtcNow.AddDays(30);
        var request = new CreateProjectMilestoneVM 
        { 
            ProjectId = _project.Id,
            Description = "New milestone",
            Amount = 500m,
            DueDate = dueDate
        };

        // Act
        var response = await Client.PostAsJsonAsync("ProjectMilestone", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var milestoneFromResponse = await JsonHelper.GetPayloadAsync<ProjectMilestoneVM>(response);
        var milestoneId = milestoneFromResponse.Id;

        var milestoneFromDb = await Context.Set<ProjectMilestone>().FirstOrDefaultAsync(x => x.Id == milestoneId);

        milestoneFromDb.Should().NotBeNull();
        milestoneFromDb.ProjectId.Should().Be(_project.Id);
        milestoneFromDb.Description.Should().Be("New milestone");
        milestoneFromDb.Amount.Should().Be(500m);
    }
    
    [Fact]
    public async Task ShouldUpdateProjectMilestone()
    {
        // Arrange
        var newDueDate = DateTime.UtcNow.AddDays(45);
        var request = new UpdateProjectMilestoneVM 
        { 
            Description = "Updated milestone",
            Amount = 750m,
            DueDate = newDueDate
        };

        // Act
        var response = await Client.PutAsJsonAsync($"ProjectMilestone/{_projectMilestone.Id}", request);
        
        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        
        var milestoneFromResponse = await JsonHelper.GetPayloadAsync<ProjectMilestoneVM>(response);
        
        var milestoneFromDb = await Context.Set<ProjectMilestone>().FirstOrDefaultAsync(x => x.Id == milestoneFromResponse.Id);
        
        milestoneFromDb.Should().NotBeNull();
        milestoneFromDb.Description.Should().Be("Updated milestone");
        milestoneFromDb.Amount.Should().Be(750m);
    }
    
    [Fact]
    public async Task ShouldDeleteProjectMilestone()
    {
        // Act
        var response = await Client.DeleteAsync($"ProjectMilestone/{_projectMilestone.Id}");
        
        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        
        var milestoneFromDb = await Context.Set<ProjectMilestone>().FirstOrDefaultAsync(x => x.Id == _projectMilestone.Id);
        
        milestoneFromDb.Should().BeNull();
    }

    [Fact]
    public async Task ShouldGetProjectMilestoneById()
    {
        // Act
        var response = await Client.GetAsync($"ProjectMilestone/{_projectMilestone.Id}");
        
        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        
        var milestoneFromResponse = await JsonHelper.GetPayloadAsync<ProjectMilestoneVM>(response);
        
        milestoneFromResponse.Should().NotBeNull();
        milestoneFromResponse.Id.Should().Be(_projectMilestone.Id);
        milestoneFromResponse.ProjectId.Should().Be(_project.Id);
    }

    [Fact]
    public async Task ShouldGetProjectMilestonesByProjectId()
    {
        // Act
        var response = await Client.GetAsync($"ProjectMilestone/by-project/{_project.Id}");
        
        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        
        var milestones = await JsonHelper.GetPayloadAsync<List<ProjectMilestoneVM>>(response);
        
        milestones.Should().NotBeEmpty();
        milestones.Should().Contain(m => m.Id == _projectMilestone.Id);
    }

    [Fact]
    public async Task ShouldNotCreateProjectMilestoneBecauseProjectNotFound()
    {
        // Arrange
        var request = new CreateProjectMilestoneVM 
        { 
            ProjectId = Guid.NewGuid(),
            Description = "Test",
            Amount = 500m,
            DueDate = DateTime.UtcNow.AddDays(30)
        };
        
        // Act
        var response = await Client.PostAsJsonAsync("ProjectMilestone", request);
        
        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldNotUpdateBecauseNotFound()
    {
        // Arrange
        var request = new UpdateProjectMilestoneVM 
        { 
            Description = "Test",
            Amount = 500m,
            DueDate = DateTime.UtcNow.AddDays(30)
        };
        
        // Act
        var response = await Client.PutAsJsonAsync($"ProjectMilestone/{Guid.NewGuid()}", request);
        
        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task ShouldNotDeleteBecauseNotFound()
    {
        // Act
        var response = await Client.DeleteAsync($"ProjectMilestone/{Guid.NewGuid()}");
        
        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task ShouldNotGetByIdBecauseNotFound()
    {
        // Act
        var response = await Client.GetAsync($"ProjectMilestone/{Guid.NewGuid()}");
        
        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task ShouldReturnEmptyListForProjectWithNoMilestones()
    {
        // Arrange
        var projectWithoutMilestones = ProjectData.CreateProject();
        await Context.AddAsync(projectWithoutMilestones);
        await SaveChangesAsync();

        // Act
        var response = await Client.GetAsync($"ProjectMilestone/by-project/{projectWithoutMilestones.Id}");
        
        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        
        var milestones = await JsonHelper.GetPayloadAsync<List<ProjectMilestoneVM>>(response);
        
        milestones.Should().BeEmpty();
    }

    public async Task InitializeAsync()
    {
        _employerUser = UserData.CreateTestUser(UserId);
        _project = ProjectData.CreateProject();
        _projectMilestone = new ProjectMilestone
        {
            Id = Guid.NewGuid(),
            ProjectId = _project.Id,
            Description = "Test milestone",
            Amount = 1000m,
            DueDate = DateTime.UtcNow.AddDays(30),
            Status = ProjectMilestoneStatus.Pending,
            CreatedBy = _employerUser.Id
        };
        
        await Context.AddAsync(_employerUser);
        await Context.AddAsync(_project);
        await Context.AddAsync(_projectMilestone);
        await SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        Context.Set<ProjectMilestone>().RemoveRange(Context.Set<ProjectMilestone>());
        Context.Set<Project>().RemoveRange(Context.Set<Project>());
        Context.Set<User>().RemoveRange(Context.Set<User>());
        await SaveChangesAsync();
    }
}
