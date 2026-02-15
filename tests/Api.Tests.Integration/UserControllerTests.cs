using System.Net;
using System.Net.Http.Json;
using BLL;
using BLL.ViewModels.User;
using DAL.Extensions;
using Domain.Models.Countries;
using Domain.Models.Languages;
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
    private Country _country = null!;
    private Language _language1 = null!;
    private Language _language2 = null!;

    [Fact]
    public async Task ShouldCreateUser()
    {
        // Arrange
        var request = new CreateUserByAdminVM
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

        var request = new CreateUserByAdminVM
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

        var request = new UpdateUserByAdminVM
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

        var request = new UpdateUserByAdminVM
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

        var request = new CreateUserByAdminVM
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


    [Fact]
    public async Task ShouldUpdateUserEmail()
    {
        // Arrange
        SwitchUser(role: Settings.Roles.AdminRole, userId: _adminUser.Id);

        var newEmail = "newemail@test.com";
        var request = new UpdateUserByAdminVM
        {
            Email = newEmail
        };

        // Act
        var response = await Client.PutAsJsonAsync($"User/{_testUser.Id}", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var userFromDb = await Context.Set<User>().FirstOrDefaultAsync(x => x.Id == _testUser.Id);
        userFromDb.Should().NotBeNull();
        userFromDb.Email.Should().Be(newEmail);
    }

    [Fact]
    public async Task ShouldUpdateUserPassword()
    {
        // Arrange
        SwitchUser(role: Settings.Roles.AdminRole, userId: _adminUser.Id);

        var oldPasswordHash = _testUser.PasswordHash;
        var request = new UpdateUserByAdminVM
        {
            Password = "NewPassword123!@#"
        };

        // Act
        var response = await Client.PutAsJsonAsync($"User/{_testUser.Id}", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var userFromDb = await Context.Set<User>().FirstOrDefaultAsync(x => x.Id == _testUser.Id);
        userFromDb.Should().NotBeNull();
        userFromDb.PasswordHash.Should().NotBe(oldPasswordHash);
        userFromDb.PasswordHash.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task ShouldUpdateUserDisplayName()
    {
        // Arrange
        SwitchUser(role: Settings.Roles.AdminRole, userId: _adminUser.Id);

        var newDisplayName = "John Doe Updated";
        var request = new UpdateUserByAdminVM
        {
            DisplayName = newDisplayName
        };

        // Act
        var response = await Client.PutAsJsonAsync($"User/{_testUser.Id}", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var userFromDb = await Context.Set<User>().FirstOrDefaultAsync(x => x.Id == _testUser.Id);
        userFromDb.Should().NotBeNull();
        userFromDb.DisplayName.Should().Be(newDisplayName);
    }

    [Fact]
    public async Task ShouldNotUpdateUserEmailToExistingEmail()
    {
        // Arrange
        var anotherUser = UserData.CreateTestUser(
            email: "another@test.com",
            roleId: Settings.Roles.FreelancerRole
        );
        await Context.AddAsync(anotherUser);
        await SaveChangesAsync();

        SwitchUser(role: Settings.Roles.AdminRole, userId: _adminUser.Id);

        var request = new UpdateUserByAdminVM
        {
            Email = anotherUser.Email // Try to use existing email
        };

        // Act
        var response = await Client.PutAsJsonAsync($"User/{_testUser.Id}", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldUpdateMultipleUserFieldsAtOnce()
    {
        // Arrange
        SwitchUser(role: Settings.Roles.AdminRole, userId: _adminUser.Id);

        var request = new UpdateUserByAdminVM
        {
            Email = "updatedemail@test.com",
            DisplayName = "Updated Name",
            Password = "NewPass123!@#",
            CountryId = _country.Id
        };

        // Act
        var response = await Client.PutAsJsonAsync($"User/{_testUser.Id}", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var userFromDb = await Context.Set<User>().FirstOrDefaultAsync(x => x.Id == _testUser.Id);
        userFromDb.Should().NotBeNull();
        userFromDb.Email.Should().Be(request.Email);
        userFromDb.DisplayName.Should().Be(request.DisplayName);
        userFromDb.CountryId.Should().Be(_country.Id);
        userFromDb.PasswordHash.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task ShouldCreateUserWithDisplayName()
    {
        // Arrange
        SwitchUser(role: Settings.Roles.AdminRole, userId: _adminUser.Id);

        var request = new CreateUserByAdminVM
        {
            Email = "userwithname@test.com",
            Password = "Test123!@#",
            RoleId = Settings.Roles.FreelancerRole,
            DisplayName = "John Doe"
        };

        // Act
        var response = await Client.PostAsJsonAsync("User", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var userFromResponse = await JsonHelper.GetPayloadAsync<UserVM>(response);
        userFromResponse.DisplayName.Should().Be(request.DisplayName);

        var userFromDb = await Context.Set<User>().FirstOrDefaultAsync(x => x.Id == userFromResponse.Id);
        userFromDb.Should().NotBeNull();
        userFromDb.DisplayName.Should().Be(request.DisplayName);
    }


    [Fact]
    public async Task ShouldReturnBadRequestWhenCreatingUserWithInvalidEmail()
    {
        // Arrange
        SwitchUser(role: Settings.Roles.AdminRole, userId: _adminUser.Id);

        var request = new CreateUserByAdminVM
        {
            Email = "invalid-email", // Invalid email format
            Password = "Test123!@#",
            RoleId = Settings.Roles.FreelancerRole
        };

        // Act
        var response = await Client.PostAsJsonAsync("User", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturnBadRequestWhenCreatingUserWithEmptyPassword()
    {
        // Arrange
        SwitchUser(role: Settings.Roles.AdminRole, userId: _adminUser.Id);

        var request = new CreateUserByAdminVM
        {
            Email = "validuser@test.com",
            Password = "", // Empty password
            RoleId = Settings.Roles.FreelancerRole
        };

        // Act
        var response = await Client.PostAsJsonAsync("User", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldUpdateUserLanguages()
    {
        SwitchUser(role: _testUser.RoleId, userId: _testUser.Id);

        // Arrange
        var request = new UpdateUserLanguagesVM
        {
            LanguageIds = new List<int> { _language1.Id, _language2.Id }
        };

        // Act
        var response = await Client.PutAsJsonAsync("User/languages", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var userFromDb = await Context.Set<User>()
            .Include(f => f.Languages)
            .FirstOrDefaultAsync(x => x.Id == _testUser.Id);

        userFromDb.Should().NotBeNull();
        userFromDb.Languages.Should().HaveCount(2);
        userFromDb.Languages.Should().Contain(l => l.Id == _language1.Id);
        userFromDb.Languages.Should().Contain(l => l.Id == _language2.Id);
    }

    [Fact]
    public async Task ShouldUpdateUserLanguagesWithEmptyList()
    {
        // Arrange
        var request = new UpdateUserLanguagesVM
        {
            LanguageIds = new List<int>()
        };

        // Act
        var response = await Client.PutAsJsonAsync("User/languages", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var userFromDb = await Context.Set<User>()
            .Include(f => f.Languages)
            .FirstOrDefaultAsync(x => x.CreatedBy == _testUser.Id);

        userFromDb.Should().NotBeNull();
        userFromDb.Languages.Should().BeEmpty();
    }


    [Fact]
    public async Task ShouldNotUpdateUserLanguagesBecauseLanguageNotFound()
    {
        // Arrange
        var request = new UpdateUserLanguagesVM
        {
            LanguageIds = new List<int> { int.MaxValue }
        };

        // Act
        var response = await Client.PutAsJsonAsync("User/languages", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldCreateUsersWithDifferentRoles()
    {
        // Arrange
        SwitchUser(role: Settings.Roles.AdminRole, userId: _adminUser.Id);

        var employerRequest = new CreateUserByAdminVM
        {
            Email = "employer@test.com",
            Password = "Test123!@#",
            RoleId = Settings.Roles.EmployerRole
        };

        var freelancerRequest = new CreateUserByAdminVM
        {
            Email = "freelancer@test.com",
            Password = "Test123!@#",
            RoleId = Settings.Roles.FreelancerRole
        };

        var moderatorRequest = new CreateUserByAdminVM
        {
            Email = "moderator@test.com",
            Password = "Test123!@#",
            RoleId = Settings.Roles.ModeratorRole
        };

        // Act
        var employerResponse = await Client.PostAsJsonAsync("User", employerRequest);
        var freelancerResponse = await Client.PostAsJsonAsync("User", freelancerRequest);
        var moderatorResponse = await Client.PostAsJsonAsync("User", moderatorRequest);

        // Assert
        employerResponse.IsSuccessStatusCode.Should().BeTrue();
        freelancerResponse.IsSuccessStatusCode.Should().BeTrue();
        moderatorResponse.IsSuccessStatusCode.Should().BeTrue();

        var employer = await JsonHelper.GetPayloadAsync<UserVM>(employerResponse);
        var freelancer = await JsonHelper.GetPayloadAsync<UserVM>(freelancerResponse);
        var moderator = await JsonHelper.GetPayloadAsync<UserVM>(moderatorResponse);

        employer.RoleId.Should().Be(Settings.Roles.EmployerRole);
        freelancer.RoleId.Should().Be(Settings.Roles.FreelancerRole);
        moderator.RoleId.Should().Be(Settings.Roles.ModeratorRole);
    }
    
    public async Task InitializeAsync()
    {
        // var employerRole = new Role { Id = Settings.Roles.EmployerRole, Name = Settings.Roles.EmployerRole };
        // var freelancerRole = new Role { Id = Settings.Roles.FreelancerRole, Name = Settings.Roles.FreelancerRole };
        // var adminRole = new Role { Id = Settings.Roles.AdminRole, Name = Settings.Roles.AdminRole, };
        
        _country = CountryData.MainCountry;
        _language1 = new Language { Id = 0, Name = "English", Code = "EN" };
        _language2 = new Language { Id = 0, Name = "Spanish", Code = "ES" };

        _adminUser = UserData.CreateTestUser(
            id: UserId,
            email: "admin@test.com",
            roleId: Settings.Roles.AdminRole
        );

        _testUser = UserData.CreateTestUser(
            email: "testuser@test.com",
            roleId: Settings.Roles.EmployerRole
        );

        // await Context.AddAsync(employerRole);
        // await Context.AddAsync(freelancerRole);
        // await Context.AddAsync(adminRole);
        await Context.AddAsync(_country);
        await Context.AddAsync(_language1);
        await Context.AddAsync(_language2);
        await Context.AddAuditableAsync(_adminUser);
        await Context.AddAuditableAsync(_testUser);
        await SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        Context.Set<User>().RemoveRange(Context.Set<User>());
        // Context.Set<Role>().RemoveRange(Context.Set<Role>());
        Context.Set<Country>().RemoveRange(Context.Set<Country>());
        Context.Set<Language>().RemoveRange(Context.Set<Language>());
        await SaveChangesAsync();
    }
}