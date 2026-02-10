using System.Net;
using System.Net.Http.Json;
using BLL.ViewModels.Dispute;
using Domain;
using Domain.Models.Contracts;
using Domain.Models.Disputes;
using Domain.Models.Freelance;
using Domain.Models.Projects;
using Domain.Models.Users;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Tests.Common;
using TestsData;

namespace Api.Tests.Integration;

public class DisputeControllerTests(IntegrationTestWebFactory factory)
    : BaseIntegrationTest(factory, customRole: Settings.Roles.ModeratorRole), IAsyncLifetime
{
    private User _employerUser = null!;
    private User _freelancerUser = null!;
    private Project _project = null!;
    private Freelancer _freelancer = null!;
    private Contract _contract = null!;
    private Dispute _existingDispute = null!;

    [Fact]
    public async Task ShouldCreateDispute()
    {
        // Arrange
        Context.Set<Dispute>().RemoveRange(Context.Set<Dispute>());
        await SaveChangesAsync();
        
        var request = new CreateDisputeVM
        {
            ContractId = _contract.Id,
            Reason = "Contract terms were not met, project deadline was missed"
        };

        // Act
        var response = await Client.PostAsJsonAsync("Dispute", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var disputeFromResponse = await JsonHelper.GetPayloadAsync<DisputeVM>(response);
        var disputeId = disputeFromResponse.Id;

        var disputeFromDb = await Context.Set<Dispute>().FirstOrDefaultAsync(x => x.Id == disputeId);

        disputeFromDb.Should().NotBeNull();
        disputeFromDb.ContractId.Should().Be(_contract.Id);
        disputeFromDb.Reason.Should().Be(request.Reason);
        disputeFromDb.Status.Should().Be(DisputeStatus.Open);
        disputeFromDb.CreatedBy.Should().Be(UserId);
        
        var contractFromDb = await Context.Set<Contract>().FirstOrDefaultAsync(x => x.Id == _contract.Id);
        contractFromDb.Should().NotBeNull();
        contractFromDb.Status.Should().Be(ContractStatus.Disputed);
    }
    
    [Fact]
    public async Task ShouldCreateDisputeByFreelancer()
    {
        // Arrange
        SwitchUser(role: Settings.Roles.FreelancerRole, userId: _freelancerUser.Id);
        var request = new CreateDisputeVM
        {
            ContractId = _contract.Id,
            Reason = "Freelancer dispute reason for testing"
        };

        // Act
        var response = await Client.PostAsJsonAsync("Dispute", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var disputeFromResponse = await JsonHelper.GetPayloadAsync<DisputeVM>(response);
        var disputeId = disputeFromResponse.Id;

        var disputeFromDb = await Context.Set<Dispute>().FirstOrDefaultAsync(x => x.Id == disputeId);

        disputeFromDb.Should().NotBeNull();
        disputeFromDb.ContractId.Should().Be(_contract.Id);
        disputeFromDb.Reason.Should().Be(request.Reason);
        disputeFromDb.Status.Should().Be(DisputeStatus.Open);
        disputeFromDb.CreatedBy.Should().Be(_freelancerUser.Id);
        
        var contractFromDb = await Context.Set<Contract>().FirstOrDefaultAsync(x => x.Id == _contract.Id);
        contractFromDb.Should().NotBeNull();
        contractFromDb.Status.Should().Be(ContractStatus.Disputed);
    }
    
    [Fact]
    public async Task ShouldNotAllowCreatingDisputeByUnauthorizedUser()
    {
        // Arrange
        var freelancerUser2 = UserData.CreateTestUser(
            email: "freelancer2@mail.com",
            roleId: Settings.Roles.FreelancerRole);
        
        var freelancer2 = FreelancerData.CreateFreelancer(userId: freelancerUser2.Id);
        
        await Context.AddAsync(freelancerUser2);
        await Context.AddAsync(freelancer2);
        await SaveChangesAsync();
        
        SwitchUser(role: Settings.Roles.EmployerRole, userId: freelancerUser2.Id); // User who is not part of the contract
        var request = new CreateDisputeVM
        {
            ContractId = _contract.Id,
            Reason = "Unauthorized user dispute reason for testing"
        };

        // Act
        var response = await Client.PostAsJsonAsync("Dispute", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task ShouldGetDisputesByUser()
    {
        // Act
        var response = await Client.GetAsync("Dispute/by-user");

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var disputesFromResponse = await JsonHelper.GetPayloadAsync<List<DisputeVM>>(response);
        
        disputesFromResponse.Should().NotBeNull();
        disputesFromResponse.Should().HaveCountGreaterThan(0);
        disputesFromResponse.Should().Contain(d => d.Id == _existingDispute.Id);
    }

    [Fact]
    public async Task ShouldGetDisputeById()
    {
        // Act
        var response = await Client.GetAsync($"Dispute/{_existingDispute.Id}");

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var disputeFromResponse = await JsonHelper.GetPayloadAsync<DisputeVM>(response);
        
        disputeFromResponse.Id.Should().Be(_existingDispute.Id);
        disputeFromResponse.ContractId.Should().Be(_existingDispute.ContractId);
        disputeFromResponse.Reason.Should().Be(_existingDispute.Reason);
        disputeFromResponse.Status.Should().Be(_existingDispute.Status);
    }

    [Fact]
    public async Task ShouldUpdateDisputeStatus()
    {
        // Arrange
        var request = new UpdateDisputeStatusForModeratorVM
        {
            Status = DisputeStatusForModerator.UnderReview
        };

        // Act
        var response = await Client.PutAsJsonAsync($"Dispute/{_existingDispute.Id}/status", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var disputeFromResponse = await JsonHelper.GetPayloadAsync<DisputeVM>(response);

        var disputeFromDb = await Context.Set<Dispute>().FirstOrDefaultAsync(x => x.Id == disputeFromResponse.Id);

        disputeFromDb.Should().NotBeNull();
        disputeFromDb.Status.Should().Be(DisputeStatus.UnderReview);
        disputeFromDb.ContractId.Should().Be(_existingDispute.ContractId);
    }

    [Fact]
    public async Task ShouldReturnBadRequestWhenCreatingDisputeWithoutContractId()
    {
        // Arrange
        var request = new CreateDisputeVM
        {
            ContractId = Guid.Empty,
            Reason = "Some reason"
        };

        // Act
        var response = await Client.PostAsJsonAsync("Dispute", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldReturnNotFoundWhenGettingNonExistentDispute()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();

        // Act
        var response = await Client.GetAsync($"Dispute/{nonExistentId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldReturnNotFoundWhenUpdatingNonExistentDispute()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();
        var request = new UpdateDisputeStatusForModeratorVM
        {
            Status = DisputeStatusForModerator.UnderReview
        };

        // Act
        var response = await Client.PutAsJsonAsync($"Dispute/{nonExistentId}/status", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldDeleteDispute()
    {
        // Arrange
        var disputeToDelete = DisputeData.CreateDispute(
            contractId: _contract.Id,
            reason: "Dispute to delete",
            createdById: UserId
        );
        await Context.AddAsync(disputeToDelete);
        await SaveChangesAsync();

        // Act
        var response = await Client.DeleteAsync($"Dispute/{disputeToDelete.Id}");

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var disputeFromDb = await Context.Set<Dispute>().FirstOrDefaultAsync(x => x.Id == disputeToDelete.Id);
        disputeFromDb.Should().BeNull();
    }

    [Fact]
    public async Task ShouldGetAllDisputes()
    {
        // Act
        var response = await Client.GetAsync("Dispute");

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var disputesFromResponse = await JsonHelper.GetPayloadAsync<List<DisputeVM>>(response);
        
        disputesFromResponse.Should().NotBeNull();
        disputesFromResponse.Should().HaveCountGreaterThan(0);
    }

    public async Task InitializeAsync()
    {
        _employerUser = UserData.CreateTestUser(
            id: UserId,
            email: "employer@test.com",
            roleId: Settings.Roles.EmployerRole
        );

        _freelancerUser = UserData.CreateTestUser(
            email: "freelancer@test.com",
            roleId: Settings.Roles.FreelancerRole
        );

        _project = ProjectData.CreateProject(userId: _employerUser.Id);
        _freelancer = FreelancerData.CreateFreelancer(userId: _freelancerUser.Id);

        _contract = ContractData.CreateContract(
            projectId: _project.Id,
            freelancerId: _freelancer.Id,
            createdById: _employerUser.Id,
            status: ContractStatus.Active
        );

        _existingDispute = DisputeData.CreateDispute(
            contractId: _contract.Id,
            reason: "Original dispute reason for testing",
            status: DisputeStatus.Open,
            createdById: _employerUser.Id
        );

        await Context.AddAsync(_employerUser);
        await Context.AddAsync(_freelancerUser);
        await Context.AddAsync(_project);
        await Context.AddAsync(_freelancer);
        await Context.AddAsync(_contract);
        await Context.AddAsync(_existingDispute);
        await SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        Context.Set<Dispute>().RemoveRange(Context.Set<Dispute>());
        Context.Set<Contract>().RemoveRange(Context.Set<Contract>());
        Context.Set<Freelancer>().RemoveRange(Context.Set<Freelancer>());
        Context.Set<Project>().RemoveRange(Context.Set<Project>());
        Context.Set<User>().RemoveRange(Context.Set<User>());
        await SaveChangesAsync();
    }
}
