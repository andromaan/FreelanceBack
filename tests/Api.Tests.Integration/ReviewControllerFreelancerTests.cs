using System.Net.Http.Json;
using BLL;
using BLL.ViewModels.Reviews;
using Domain.Models.Contracts;
using Domain.Models.Freelance;
using Domain.Models.Projects;
using Domain.Models.Reviews;
using Domain.Models.Users;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Tests.Common;
using TestsData;

namespace Api.Tests.Integration;

public class ReviewControllerFreelancerTests(IntegrationTestWebFactory factory)
    : BaseIntegrationTest(factory, customRole: Settings.Roles.FreelancerRole), IAsyncLifetime
{
    private User _employerUser = null!;
    private User _freelancerUser = null!;
    private Project _project = null!;
    private Freelancer _freelancer = null!;
    private Contract _contract = null!;
    private Review _existingReview = null!;

    [Fact]
    public async Task ShouldCreateReview()
    {
        // Arrange
        Context.Set<Review>().RemoveRange(Context.Set<Review>());
        await SaveChangesAsync();
        
        var request = new CreateReviewVM
        {
            ContractId = _contract.Id,
            Rating = 4.5m,
            ReviewText = "Excellent employer, great to work with!"
        };

        // Act
        var response = await Client.PostAsJsonAsync("Review", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var reviewFromResponse = await JsonHelper.GetPayloadAsync<ReviewVM>(response);
        var reviewId = reviewFromResponse.Id;

        var reviewFromDb = await Context.Set<Review>().FirstOrDefaultAsync(x => x.Id == reviewId);

        reviewFromDb.Should().NotBeNull();
        reviewFromDb.ContractId.Should().Be(_contract.Id);
        reviewFromDb.ReviewedUserId.Should().Be(_employerUser.Id);
        reviewFromDb.ReviewerRoleId.Should().Be(_freelancerUser.RoleId);
        reviewFromDb.Rating.Should().Be(request.Rating);
        reviewFromDb.ReviewText.Should().Be(request.ReviewText);
        reviewFromDb.CreatedBy.Should().Be(UserId);
    }

    public async Task InitializeAsync()
    {
        // Set employer user to the same UserId as the JWT token
        _employerUser = UserData.CreateTestUser(email: "employer@test.com");
        _employerUser.RoleId = Settings.Roles.EmployerRole;
        
        _freelancerUser = UserData.CreateTestUser(email: "freelancer@test.com");
        _freelancerUser.RoleId = Settings.Roles.FreelancerRole;
        _freelancerUser.Id = UserId;
        
        _project = ProjectData.CreateProject(userId: _employerUser.Id);
        _freelancer = FreelancerData.CreateFreelancer(userId: _freelancerUser.Id);
        
        _contract = ContractData.CreateContract(
            projectId: _project.Id,
            freelancerId: _freelancer.Id,
            agreedRate: 2000m,
            createdById: _employerUser.Id
        );
        _contract.Status = ContractStatus.Completed;
        
        _existingReview = ReviewData.CreateReview(
            contractId: _contract.Id,
            reviewedUserId: _employerUser.Id,
            rating: 4.0m,
            reviewText: "Good employer, but communication could be better.",
            reviewerRoleId: Settings.Roles.FreelancerRole,
            createdById: _freelancerUser.Id
        );

        await Context.AddAsync(_employerUser);
        await Context.AddAsync(_freelancerUser);
        await Context.AddAsync(_project);
        await Context.AddAsync(_freelancer);
        await Context.AddAsync(_contract);
        await Context.AddAsync(_existingReview);
        await SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        Context.Set<Review>().RemoveRange(Context.Set<Review>());
        Context.Set<Contract>().RemoveRange(Context.Set<Contract>());
        Context.Set<Freelancer>().RemoveRange(Context.Set<Freelancer>());
        Context.Set<Project>().RemoveRange(Context.Set<Project>());
        Context.Set<User>().RemoveRange(Context.Set<User>());
        await SaveChangesAsync();
    }
}
