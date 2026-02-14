using System.Net;
using System.Net.Http.Json;
using BLL;
using BLL.ViewModels.User;
using DAL.Extensions;
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

    [Fact]
    public async Task ShouldUpdateUserAvatar()
    {
        // Arrange
        SwitchUser(role: Settings.Roles.EmployerRole, userId: _testUser.Id);

        var imageContent = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }; // PNG header
        using var content = new MultipartFormDataContent();
        using var fileContent = new ByteArrayContent(imageContent);
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
        content.Add(fileContent, "file", "avatar.png");

        // Act
        var response = await Client.PatchAsync("User/update-avatar", content);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var userFromResponse = await JsonHelper.GetPayloadAsync<UserVM>(response);
        userFromResponse.AvatarImg.Should().NotBeNullOrEmpty();

        var userFromDb = await Context.Set<User>().FirstOrDefaultAsync(x => x.Id == _testUser.Id);
        userFromDb.Should().NotBeNull();
        userFromDb.AvatarImg.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task ShouldUpdateUserAvatarMultipleTimes()
    {
        // Arrange
        SwitchUser(role: Settings.Roles.FreelancerRole, userId: _testUser.Id);

        var imageContent1 = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
        using var content1 = new MultipartFormDataContent();
        using var fileContent1 = new ByteArrayContent(imageContent1);
        fileContent1.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
        content1.Add(fileContent1, "file", "avatar1.png");

        // Act - First upload
        var response1 = await Client.PatchAsync("User/update-avatar", content1);
        response1.IsSuccessStatusCode.Should().BeTrue();
        var user1 = await JsonHelper.GetPayloadAsync<UserVM>(response1);
        var firstAvatarPath = user1.AvatarImg;

        // Act - Second upload
        var imageContent2 = new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 }; // JPEG header
        using var content2 = new MultipartFormDataContent();
        using var fileContent2 = new ByteArrayContent(imageContent2);
        fileContent2.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
        content2.Add(fileContent2, "file", "avatar2.jpg");

        var response2 = await Client.PatchAsync("User/update-avatar", content2);

        // Assert
        response2.IsSuccessStatusCode.Should().BeTrue();
        var user2 = await JsonHelper.GetPayloadAsync<UserVM>(response2);
        user2.AvatarImg.Should().NotBeNullOrEmpty();
        user2.AvatarImg.Should().NotBe(firstAvatarPath); // Should be different
    }

    [Fact]
    public async Task ShouldNotUpdateAvatarWithoutAuthentication()
    {
        // Arrange
        Client.DefaultRequestHeaders.Authorization = null;

        var imageContent = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
        using var content = new MultipartFormDataContent();
        using var fileContent = new ByteArrayContent(imageContent);
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
        content.Add(fileContent, "file", "avatar.png");

        // Act
        var response = await Client.PatchAsync("User/update-avatar", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task ShouldUpdateUserEmail()
    {
        // Arrange
        SwitchUser(role: Settings.Roles.AdminRole, userId: _adminUser.Id);
        
        var newEmail = "newemail@test.com";
        var request = new UpdateUserVM
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
        var request = new UpdateUserVM
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
        var request = new UpdateUserVM
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
        
        var request = new UpdateUserVM
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
        
        var request = new UpdateUserVM
        {
            Email = "updatedemail@test.com",
            DisplayName = "Updated Name",
            Password = "NewPass123!@#"
        };

        // Act
        var response = await Client.PutAsJsonAsync($"User/{_testUser.Id}", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var userFromDb = await Context.Set<User>().FirstOrDefaultAsync(x => x.Id == _testUser.Id);
        userFromDb.Should().NotBeNull();
        userFromDb.Email.Should().Be(request.Email);
        userFromDb.DisplayName.Should().Be(request.DisplayName);
        userFromDb.PasswordHash.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task ShouldCreateUserWithDisplayName()
    {
        // Arrange
        SwitchUser(role: Settings.Roles.AdminRole, userId: _adminUser.Id);
        
        var request = new CreateUserVM
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
    public async Task ShouldGetMyselfWithAvatar()
    {
        // Arrange
        SwitchUser(role: Settings.Roles.EmployerRole, userId: _testUser.Id);

        // First upload avatar
        var imageContent = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
        using var content = new MultipartFormDataContent();
        using var fileContent = new ByteArrayContent(imageContent);
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
        content.Add(fileContent, "file", "avatar.png");
        await Client.PatchAsync("User/update-avatar", content);

        // Act
        var response = await Client.GetAsync("User/get-myself");

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var userFromResponse = await JsonHelper.GetPayloadAsync<UserVM>(response);
        userFromResponse.AvatarImg.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task ShouldReturnBadRequestWhenCreatingUserWithInvalidEmail()
    {
        // Arrange
        SwitchUser(role: Settings.Roles.AdminRole, userId: _adminUser.Id);
        
        var request = new CreateUserVM
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
        
        var request = new CreateUserVM
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
    public async Task ShouldCreateUsersWithDifferentRoles()
    {
        // Arrange
        SwitchUser(role: Settings.Roles.AdminRole, userId: _adminUser.Id);

        var employerRequest = new CreateUserVM
        {
            Email = "employer@test.com",
            Password = "Test123!@#",
            RoleId = Settings.Roles.EmployerRole
        };

        var freelancerRequest = new CreateUserVM
        {
            Email = "freelancer@test.com",
            Password = "Test123!@#",
            RoleId = Settings.Roles.FreelancerRole
        };

        var moderatorRequest = new CreateUserVM
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

    [Fact]
    public async Task ShouldUpdateAvatarWithPngFormat()
    {
        // Arrange
        SwitchUser(role: Settings.Roles.EmployerRole, userId: _testUser.Id);

        var pngHeader = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
        using var content = new MultipartFormDataContent();
        using var fileContent = new ByteArrayContent(pngHeader);
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
        content.Add(fileContent, "file", "avatar.png");

        // Act
        var response = await Client.PatchAsync("User/update-avatar", content);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        var user = await JsonHelper.GetPayloadAsync<UserVM>(response);
        user.AvatarImg.Should().NotBeNullOrEmpty();
        user.AvatarImg.Should().Match(img => img.Contains(".png") || img.Contains(".jpg"));
    }

    [Fact]
    public async Task ShouldUpdateAvatarWithJpegFormat()
    {
        // Arrange
        SwitchUser(role: Settings.Roles.FreelancerRole, userId: _testUser.Id);

        var jpegHeader = new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 };
        using var content = new MultipartFormDataContent();
        using var fileContent = new ByteArrayContent(jpegHeader);
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
        content.Add(fileContent, "file", "avatar.jpg");

        // Act
        var response = await Client.PatchAsync("User/update-avatar", content);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        var user = await JsonHelper.GetPayloadAsync<UserVM>(response);
        user.AvatarImg.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task ShouldUpdateAvatarWithJpgExtension()
    {
        // Arrange
        SwitchUser(role: Settings.Roles.ModeratorRole, userId: _adminUser.Id);

        var jpegHeader = new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 };
        using var content = new MultipartFormDataContent();
        using var fileContent = new ByteArrayContent(jpegHeader);
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
        content.Add(fileContent, "file", "avatar.jpg");

        // Act
        var response = await Client.PatchAsync("User/update-avatar", content);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        var user = await JsonHelper.GetPayloadAsync<UserVM>(response);
        user.AvatarImg.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task ShouldUpdateAvatarAndReplaceOldOne()
    {
        // Arrange
        SwitchUser(role: Settings.Roles.EmployerRole, userId: _testUser.Id);

        // First upload
        var firstImage = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
        using var content1 = new MultipartFormDataContent();
        using var fileContent1 = new ByteArrayContent(firstImage);
        fileContent1.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
        content1.Add(fileContent1, "file", "first.png");

        var response1 = await Client.PatchAsync("User/update-avatar", content1);
        response1.IsSuccessStatusCode.Should().BeTrue();
        var user1 = await JsonHelper.GetPayloadAsync<UserVM>(response1);
        var firstPath = user1.AvatarImg;

        // Second upload should replace the first
        var secondImage = new byte[] { 0xFF, 0xD8, 0xFF, 0xE0, 0x00, 0x10 };
        using var content2 = new MultipartFormDataContent();
        using var fileContent2 = new ByteArrayContent(secondImage);
        fileContent2.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
        content2.Add(fileContent2, "file", "second.jpg");

        // Act
        var response2 = await Client.PatchAsync("User/update-avatar", content2);

        // Assert
        response2.IsSuccessStatusCode.Should().BeTrue();
        var user2 = await JsonHelper.GetPayloadAsync<UserVM>(response2);
        user2.AvatarImg.Should().NotBeNullOrEmpty();
        user2.AvatarImg.Should().NotBe(firstPath);
    }

    [Fact]
    public async Task ShouldNotUpdateAvatarForDifferentUser()
    {
        // Arrange
        var otherUser = UserData.CreateTestUser(
            email: "otheruser@test.com",
            roleId: Settings.Roles.FreelancerRole
        );
        await Context.AddAsync(otherUser);
        await SaveChangesAsync();

        // Login as testUser but try to access another user's data
        SwitchUser(role: Settings.Roles.FreelancerRole, userId: _testUser.Id);

        var imageContent = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
        using var content = new MultipartFormDataContent();
        using var fileContent = new ByteArrayContent(imageContent);
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
        content.Add(fileContent, "file", "avatar.png");

        // Act - update avatar should only update current user's avatar
        var response = await Client.PatchAsync("User/update-avatar", content);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        
        var testUserFromDb = await Context.Set<User>().FirstOrDefaultAsync(x => x.Id == _testUser.Id);
        var otherUserFromDb = await Context.Set<User>().FirstOrDefaultAsync(x => x.Id == otherUser.Id);
        
        testUserFromDb!.AvatarImg.Should().NotBeNullOrEmpty();
        otherUserFromDb!.AvatarImg.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task ShouldUpdateAvatarForFreelancer()
    {
        // Arrange
        var freelancerUser = UserData.CreateTestUser(
            email: "freelancer-avatar@test.com",
            roleId: Settings.Roles.FreelancerRole
        );
        await Context.AddAsync(freelancerUser);
        await SaveChangesAsync();

        SwitchUser(role: Settings.Roles.FreelancerRole, userId: freelancerUser.Id);

        var imageContent = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
        using var content = new MultipartFormDataContent();
        using var fileContent = new ByteArrayContent(imageContent);
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
        content.Add(fileContent, "file", "freelancer-avatar.png");

        // Act
        var response = await Client.PatchAsync("User/update-avatar", content);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        var user = await JsonHelper.GetPayloadAsync<UserVM>(response);
        user.AvatarImg.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task ShouldUpdateAvatarForEmployer()
    {
        // Arrange
        var employerUser = UserData.CreateTestUser(
            email: "employer-avatar@test.com",
            roleId: Settings.Roles.EmployerRole
        );
        await Context.AddAsync(employerUser);
        await SaveChangesAsync();

        SwitchUser(role: Settings.Roles.EmployerRole, userId: employerUser.Id);

        var imageContent = new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 };
        using var content = new MultipartFormDataContent();
        using var fileContent = new ByteArrayContent(imageContent);
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
        content.Add(fileContent, "file", "employer-avatar.jpg");

        // Act
        var response = await Client.PatchAsync("User/update-avatar", content);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        var user = await JsonHelper.GetPayloadAsync<UserVM>(response);
        user.AvatarImg.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task ShouldUpdateAvatarForModerator()
    {
        // Arrange
        var moderatorUser = UserData.CreateTestUser(
            email: "moderator-avatar@test.com",
            roleId: Settings.Roles.ModeratorRole
        );
        await Context.AddAsync(moderatorUser);
        await SaveChangesAsync();

        SwitchUser(role: Settings.Roles.ModeratorRole, userId: moderatorUser.Id);

        var imageContent = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
        using var content = new MultipartFormDataContent();
        using var fileContent = new ByteArrayContent(imageContent);
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
        content.Add(fileContent, "file", "moderator-avatar.png");

        // Act
        var response = await Client.PatchAsync("User/update-avatar", content);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        var user = await JsonHelper.GetPayloadAsync<UserVM>(response);
        user.AvatarImg.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task ShouldUpdateAvatarAndGetItInSubsequentRequests()
    {
        // Arrange
        SwitchUser(role: Settings.Roles.EmployerRole, userId: _testUser.Id);

        var imageContent = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
        using var content = new MultipartFormDataContent();
        using var fileContent = new ByteArrayContent(imageContent);
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
        content.Add(fileContent, "file", "persistent-avatar.png");

        // Act - Upload avatar
        var uploadResponse = await Client.PatchAsync("User/update-avatar", content);
        uploadResponse.IsSuccessStatusCode.Should().BeTrue();
        var uploadedUser = await JsonHelper.GetPayloadAsync<UserVM>(uploadResponse);
        var avatarPath = uploadedUser.AvatarImg;

        // Act - Get user info again
        var getMyselfResponse = await Client.GetAsync("User/get-myself");
        getMyselfResponse.IsSuccessStatusCode.Should().BeTrue();
        var retrievedUser = await JsonHelper.GetPayloadAsync<UserVM>(getMyselfResponse);

        // Assert
        retrievedUser.AvatarImg.Should().Be(avatarPath);
        retrievedUser.AvatarImg.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task ShouldUpdateAvatarThreeTimes()
    {
        // Arrange
        SwitchUser(role: Settings.Roles.FreelancerRole, userId: _testUser.Id);

        var image1 = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
        using var content1 = new MultipartFormDataContent();
        using var fileContent1 = new ByteArrayContent(image1);
        fileContent1.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
        content1.Add(fileContent1, "file", "avatar1.png");

        var image2 = new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 };
        using var content2 = new MultipartFormDataContent();
        using var fileContent2 = new ByteArrayContent(image2);
        fileContent2.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
        content2.Add(fileContent2, "file", "avatar2.jpg");

        var image3 = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A, 0x00, 0x00 };
        using var content3 = new MultipartFormDataContent();
        using var fileContent3 = new ByteArrayContent(image3);
        fileContent3.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
        content3.Add(fileContent3, "file", "avatar3.png");

        // Act
        var response1 = await Client.PatchAsync("User/update-avatar", content1);
        var response2 = await Client.PatchAsync("User/update-avatar", content2);
        var response3 = await Client.PatchAsync("User/update-avatar", content3);

        // Assert
        response1.IsSuccessStatusCode.Should().BeTrue();
        response2.IsSuccessStatusCode.Should().BeTrue();
        response3.IsSuccessStatusCode.Should().BeTrue();

        var user1 = await JsonHelper.GetPayloadAsync<UserVM>(response1);
        var user2 = await JsonHelper.GetPayloadAsync<UserVM>(response2);
        var user3 = await JsonHelper.GetPayloadAsync<UserVM>(response3);

        user1.AvatarImg.Should().NotBeNullOrEmpty();
        user2.AvatarImg.Should().NotBeNullOrEmpty();
        user3.AvatarImg.Should().NotBeNullOrEmpty();

        // Each upload should have different path
        user2.AvatarImg.Should().NotBe(user1.AvatarImg);
        user3.AvatarImg.Should().NotBe(user2.AvatarImg);
    }

    [Fact]
    public async Task ShouldNotUpdateAvatarWithEmptyFile()
    {
        // Arrange
        SwitchUser(role: Settings.Roles.EmployerRole, userId: _testUser.Id);

        using var content = new MultipartFormDataContent();
        using var fileContent = new ByteArrayContent(Array.Empty<byte>());
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
        content.Add(fileContent, "file", "empty.png");

        // Act
        var response = await Client.PatchAsync("User/update-avatar", content);

        // Assert
        response.StatusCode.Should().BeOneOf(HttpStatusCode.BadRequest, HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task ShouldGetAllUsersWithAvatars()
    {
        // Arrange
        var user1 = UserData.CreateTestUser(email: "user1@test.com", roleId: Settings.Roles.FreelancerRole);
        var user2 = UserData.CreateTestUser(email: "user2@test.com", roleId: Settings.Roles.EmployerRole);
        
        await Context.AddAsync(user1);
        await Context.AddAsync(user2);
        await SaveChangesAsync();

        // Upload avatars for both users
        SwitchUser(role: Settings.Roles.FreelancerRole, userId: user1.Id);
        var image1 = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
        using var content1 = new MultipartFormDataContent();
        using var fileContent1 = new ByteArrayContent(image1);
        fileContent1.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
        content1.Add(fileContent1, "file", "avatar1.png");
        await Client.PatchAsync("User/update-avatar", content1);

        SwitchUser(role: Settings.Roles.EmployerRole, userId: user2.Id);
        var image2 = new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 };
        using var content2 = new MultipartFormDataContent();
        using var fileContent2 = new ByteArrayContent(image2);
        fileContent2.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
        content2.Add(fileContent2, "file", "avatar2.jpg");
        await Client.PatchAsync("User/update-avatar", content2);

        // Act - Get all users as admin
        SwitchUser(role: Settings.Roles.AdminRole, userId: _adminUser.Id);
        var response = await Client.GetAsync("User");

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        var users = await JsonHelper.GetPayloadAsync<List<UserVM>>(response);
        
        var userWithAvatar1 = users.FirstOrDefault(u => u.Id == user1.Id);
        var userWithAvatar2 = users.FirstOrDefault(u => u.Id == user2.Id);

        userWithAvatar1!.AvatarImg.Should().NotBeNullOrEmpty();
        userWithAvatar2!.AvatarImg.Should().NotBeNullOrEmpty();
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

        await Context.AddAuditableAsync(_adminUser);
        await Context.AddAuditableAsync(_testUser);
        await SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        // Просто очищаємо ChangeTracker, щоб уникнути проблем із залежностями
        Context.ChangeTracker.Clear();
        await Task.CompletedTask;
    }
}
















