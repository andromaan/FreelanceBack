using System.Net;
using System.Net.Http.Json;
using BLL;
using BLL.ViewModels.User;
using Domain.Models.Users;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Tests.Common;
using TestsData;

namespace Api.Tests.Integration;

public class UserControllerTests(IntegrationTestWebFactory factory)
    : BaseIntegrationTest(factory, customRole: Settings.Roles.AdminRole), IAsyncLifetime
{
    private User _testUser = null!;
    private User _adminUser = null!;

    [Fact]
    public async Task ShouldCreateUser()
    {
        // Arrange
        var request = new CreateUserVM
        {
            Email = "newuser@test.com",
            Password = "Test123!@#",
            RoleId = Settings.Roles.EmployerRole
        };

        // Act
        var response = await Client.PostAsJsonAsync("User", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var userFromResponse = await JsonHelper.GetPayloadAsync<UserVM>(response);
        var userId = userFromResponse.Id;

        var userFromDb = await Context.Set<User>().FirstOrDefaultAsync(x => x.Id == userId);

        userFromDb.Should().NotBeNull();
        userFromDb.Email.Should().Be(request.Email);
        userFromDb.RoleId.Should().Be(request.RoleId);
        userFromDb.PasswordHash.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task ShouldNotCreateUserWithoutAdminRole()
    {
        // Arrange
        SwitchUser(role: Settings.Roles.EmployerRole, userId: _testUser.Id);
        
        var request = new CreateUserVM
        {
            Email = "unauthorized@test.com",
            Password = "Test123!@#",
            RoleId = Settings.Roles.FreelancerRole
        };

        // Act
        var response = await Client.PostAsJsonAsync("User", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task ShouldGetUserById()
    {
        // Arrange
        SwitchUser(role: Settings.Roles.AdminRole, userId: _adminUser.Id);

        // Act
        var response = await Client.GetAsync($"User/{_testUser.Id}");

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var userFromResponse = await JsonHelper.GetPayloadAsync<UserVM>(response);
        
        userFromResponse.Id.Should().Be(_testUser.Id);
        userFromResponse.Email.Should().Be(_testUser.Email);
        userFromResponse.RoleId.Should().Be(_testUser.RoleId);
    }

    [Fact]
    public async Task ShouldNotGetUserByIdWithoutAdminRole()
    {
        // Arrange
        SwitchUser(role: Settings.Roles.FreelancerRole, userId: _testUser.Id);

        // Act
        var response = await Client.GetAsync($"User/{_testUser.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task ShouldGetMyself()
    {
        // Arrange
        SwitchUser(role: Settings.Roles.EmployerRole, userId: _testUser.Id);

        // Act
        var response = await Client.GetAsync("User/get-myself");

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var userFromResponse = await JsonHelper.GetPayloadAsync<UserVM>(response);
        
        userFromResponse.Id.Should().Be(_testUser.Id);
        userFromResponse.Email.Should().Be(_testUser.Email);
        userFromResponse.RoleId.Should().Be(_testUser.RoleId);
    }

    [Fact]
    public async Task ShouldGetAllUsers()
    {
        // Arrange
        SwitchUser(role: Settings.Roles.AdminRole, userId: _adminUser.Id);

        // Act
        var response = await Client.GetAsync("User");

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var usersFromResponse = await JsonHelper.GetPayloadAsync<List<UserVM>>(response);
        
        usersFromResponse.Should().NotBeNull();
        usersFromResponse.Should().HaveCountGreaterThan(0);
        usersFromResponse.Should().Contain(u => u.Id == _testUser.Id);
    }

    [Fact]
    public async Task ShouldNotGetAllUsersWithoutAdminRole()
    {
        // Arrange
        SwitchUser(role: Settings.Roles.FreelancerRole, userId: _testUser.Id);

        // Act
        var response = await Client.GetAsync("User");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task ShouldNotUpdateUserWithoutAdminRole()
    {
        // Arrange
        SwitchUser(role: Settings.Roles.EmployerRole, userId: _testUser.Id);
        
        var request = new UpdateUserVM
        {
            Email = "hacker@test.com"
        };

        // Act
        var response = await Client.PutAsJsonAsync($"User/{_testUser.Id}", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task ShouldDeleteUser()
    {
        // Arrange
        var userToDelete = UserData.CreateTestUser(
            email: "delete@test.com",
            roleId: Settings.Roles.FreelancerRole
        );
        await Context.AddAsync(userToDelete);
        await SaveChangesAsync();

        SwitchUser(role: Settings.Roles.AdminRole, userId: _adminUser.Id);

        // Act
        var response = await Client.DeleteAsync($"User/{userToDelete.Id}");

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var userFromDb = await Context.Set<User>().FirstOrDefaultAsync(x => x.Id == userToDelete.Id);
        userFromDb.Should().BeNull();
    }

    [Fact]
    public async Task ShouldNotDeleteUserWithoutAdminRole()
    {
        // Arrange
        SwitchUser(role: Settings.Roles.FreelancerRole, userId: _testUser.Id);

        // Act
        var response = await Client.DeleteAsync($"User/{_testUser.Id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task ShouldReturnNotFoundWhenGettingNonExistentUser()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();
        SwitchUser(role: Settings.Roles.AdminRole, userId: _adminUser.Id);

        // Act
        var response = await Client.GetAsync($"User/{nonExistentId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldReturnNotFoundWhenUpdatingNonExistentUser()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();
        SwitchUser(role: Settings.Roles.AdminRole, userId: _adminUser.Id);
        
        var request = new UpdateUserVM
        {
            Email = "nonexistent@test.com"
        };

        // Act
        var response = await Client.PutAsJsonAsync($"User/{nonExistentId}", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldReturnNotFoundWhenDeletingNonExistentUser()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();
        SwitchUser(role: Settings.Roles.AdminRole, userId: _adminUser.Id);

        // Act
        var response = await Client.DeleteAsync($"User/{nonExistentId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldReturnBadRequestWhenCreatingUserWithExistingEmail()
    {
        // Arrange
        SwitchUser(role: Settings.Roles.AdminRole, userId: _adminUser.Id);
        
        var request = new CreateUserVM
        {
            Email = _testUser.Email, // Existing email
            Password = "Test123!@#",
            RoleId = Settings.Roles.FreelancerRole
        };

        // Act
        var response = await Client.PostAsJsonAsync("User", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    public async Task InitializeAsync()
    {
        _adminUser = UserData.CreateTestUser(
            id: UserId,
            email: "admin@test.com",
            roleId: Settings.Roles.AdminRole
        );

        _testUser = UserData.CreateTestUser(
            email: "testuser@test.com",
            roleId: Settings.Roles.EmployerRole
        );

        await Context.AddAsync(_adminUser);
        await Context.AddAsync(_testUser);
        await SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        // Просто очищаємо ChangeTracker, щоб уникнути проблем із залежностями
        Context.ChangeTracker.Clear();
        await Task.CompletedTask;
    }
}







