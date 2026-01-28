using System.Net;
using System.Net.Http.Json;
using BLL.ViewModels.Project;
using Domain;
using Domain.Models.Auth.Users;
using Domain.Models.Projects;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Tests.Common;
using TestsData;

namespace Api.Tests.Integration;

public class ProjectControllerTests(IntegrationTestWebFactory factory)
    : BaseIntegrationTest(factory), IAsyncLifetime
{
    private Project _project = null!;
    private User _employerUser = null!;

    [Fact]
    public async Task ShouldCreateProject()
    {
        // Arrange
        var projectTitle = "New Test Project";
        var request = new CreateProjectVM 
        { 
            Title = projectTitle,
            Description = "New Test Project Description",
            BudgetMin = 2000m,
            BudgetMax = 10000m,
            IsHourly = false
        };

        // Act
        var response = await Client.PostAsJsonAsync("Project", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var projectFromResponse = await JsonHelper.GetPayloadAsync<ProjectVM>(response);
        var projectId = projectFromResponse.Id;

        var projectFromDb = await Context.Set<Project>().FirstOrDefaultAsync(x => x.Id == projectId);

        projectFromDb.Should().NotBeNull();
        projectFromDb.Title.Should().Be(projectTitle);
        projectFromDb.Description.Should().Be("New Test Project Description");
        projectFromDb.BudgetMin.Should().Be(2000m);
        projectFromDb.BudgetMax.Should().Be(10000m);
    }
    
    [Fact]
    public async Task ShouldUpdateProject()
    {
        // Arrange
        var projectTitle = "Updated Project";
        var request = new UpdateProjectVM 
        { 
            Title = projectTitle,
            Description = "Updated Description",
            BudgetMin = 3000m,
            BudgetMax = 15000m,
            IsHourly = true
        };

        // Act
        var response = await Client.PutAsJsonAsync($"Project/{_project.Id}", request);
        
        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        
        var projectFromResponse = await JsonHelper.GetPayloadAsync<ProjectVM>(response);
        
        var projectFromDb = await Context.Set<Project>().FirstOrDefaultAsync(x => x.Id == projectFromResponse.Id);
        
        projectFromDb.Should().NotBeNull();
        projectFromDb.Title.Should().Be(projectTitle);
        projectFromDb.Description.Should().Be("Updated Description");
        projectFromDb.IsHourly.Should().BeTrue();
    }
    
    [Fact]
    public async Task ShouldDeleteProject()
    {
        // Act
        var response = await Client.DeleteAsync($"Project/{_project.Id}");
        
        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        
        var projectFromDb = await Context.Set<Project>().FirstOrDefaultAsync(x => x.Id == _project.Id);
        
        projectFromDb.Should().BeNull();
    }

    [Fact]
    public async Task ShouldGetProjectById()
    {
        // Act
        var response = await Client.GetAsync($"Project/{_project.Id}");
        
        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        
        var projectFromResponse = await JsonHelper.GetPayloadAsync<ProjectVM>(response);
        
        projectFromResponse.Should().NotBeNull();
        projectFromResponse.Id.Should().Be(_project.Id);
        projectFromResponse.Title.Should().Be(_project.Title);
    }

    [Fact]
    public async Task ShouldNotUpdateBecauseNotFound()
    {
        // Arrange
        var request = new UpdateProjectVM 
        { 
            Title = "Test",
            Description = "Test",
            BudgetMin = 1000m,
            BudgetMax = 5000m,
            IsHourly = false
        };
        
        // Act
        var response = await Client.PutAsJsonAsync($"Project/{Guid.NewGuid()}", request);
        
        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task ShouldNotDeleteBecauseNotFound()
    {
        // Act
        var response = await Client.DeleteAsync($"Project/{Guid.NewGuid()}");
        
        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task ShouldNotGetByIdBecauseNotFound()
    {
        // Act
        var response = await Client.GetAsync($"Project/{Guid.NewGuid()}");
        
        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task ShouldGetAllProjects()
    {
        // Act
        var response = await Client.GetAsync("Project");
        
        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        
        var projects = await JsonHelper.GetPayloadAsync<List<ProjectVM>>(response);
        
        projects.Should().NotBeEmpty();
    }

    [Fact]
    public async Task ShouldGetProjectsByEmployer()
    {
        // Act
        var response = await Client.GetAsync("Project/by-employer");
        
        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        
        var projects = await JsonHelper.GetPayloadAsync<List<ProjectVM>>(response);
        
        projects.Should().NotBeNull();
    }

    [Fact]
    public async Task ShouldUpdateProjectCategories()
    {
        // Arrange
        var category = new Category { Id = 0, Name = "TestCategory" };
        await Context.AddAsync(category);
        await SaveChangesAsync();

        var request = new UpdateProjectCategoriesVM 
        { 
            CategoryIds = new List<int> { category.Id } 
        };

        // Act
        var response = await Client.PatchAsync($"Project/categories/{_project.Id}", 
            JsonContent.Create(request));
        
        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
    }

    public async Task InitializeAsync()
    {
        _employerUser = UserData.CreateTestUser(UserId, roleId: Settings.Roles.EmployerRole);
        _project = ProjectData.CreateProject(userId: UserId);
        
        await Context.AddAsync(_employerUser);
        await Context.AddAsync(_project);
        await SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        Context.Set<Project>().RemoveRange(Context.Set<Project>());
        Context.Set<Category>().RemoveRange(Context.Set<Category>());
        Context.Set<User>().RemoveRange(Context.Set<User>());
        await SaveChangesAsync();
    }
}
