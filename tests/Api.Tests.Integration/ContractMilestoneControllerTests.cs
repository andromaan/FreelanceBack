using System.Net;
using System.Net.Http.Json;
using BLL.ViewModels.ContractMilestone;
using Domain;
using Domain.Models.Freelance;
using Domain.Models.Projects;
using Domain.Models.Users;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Tests.Common;
using TestsData;

namespace Api.Tests.Integration;

public class ContractMilestoneControllerTests(IntegrationTestWebFactory factory)
    : BaseIntegrationTest(factory, customRole: Settings.Roles.FreelancerRole), IAsyncLifetime
{
    private ContractMilestone _contractMilestone = null!;
    private Contract _contract = null!;
    private Project _project = null!;
    private User _freelancerUser = null!;
    private Freelancer _freelancer = null!;

    [Fact]
    public async Task ShouldCreateContractMilestone()
    {
        // Arrange
        var dueDate = DateTime.UtcNow.AddDays(30);
        var request = new CreateContractMilestoneVM
        {
            ContractId = _contract.Id,
            Description = "New contract milestone",
            Amount = _contract.AgreedRate - _contractMilestone.Amount,
            DueDate = dueDate
        };

        // Act
        var response = await Client.PostAsJsonAsync("ContractMilestone", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var milestoneFromResponse = await JsonHelper.GetPayloadAsync<ContractMilestoneVM>(response);
        var milestoneId = milestoneFromResponse.Id;

        var milestoneFromDb = await Context.Set<ContractMilestone>().FirstOrDefaultAsync(x => x.Id == milestoneId);

        milestoneFromDb.Should().NotBeNull();
        milestoneFromDb.ContractId.Should().Be(_contract.Id);
        milestoneFromDb.Description.Should().Be("New contract milestone");
        milestoneFromDb.Amount.Should().Be(request.Amount);
    }

    [Fact]
    public async Task ShouldUpdateContractMilestone()
    {
        // Arrange
        var newDueDate = DateTime.UtcNow.AddDays(45);
        var request = new UpdateContractMilestoneVM
        {
            Description = "Updated contract milestone",
            Amount = 800m,
            DueDate = newDueDate
        };

        // Act
        var response = await Client.PutAsJsonAsync($"ContractMilestone/{_contractMilestone.Id}", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var milestoneFromResponse = await JsonHelper.GetPayloadAsync<ContractMilestoneVM>(response);

        var milestoneFromDb = await Context.Set<ContractMilestone>()
            .FirstOrDefaultAsync(x => x.Id == milestoneFromResponse.Id);

        milestoneFromDb.Should().NotBeNull();
        milestoneFromDb.Description.Should().Be("Updated contract milestone");
        milestoneFromDb.Amount.Should().Be(800m);
    }

    [Fact]
    public async Task ShouldDeleteContractMilestone()
    {
        // Act
        var response = await Client.DeleteAsync($"ContractMilestone/{_contractMilestone.Id}");

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var milestoneFromDb =
            await Context.Set<ContractMilestone>().FirstOrDefaultAsync(x => x.Id == _contractMilestone.Id);

        milestoneFromDb.Should().BeNull();
    }

    [Fact]
    public async Task ShouldGetContractMilestoneById()
    {
        // Act
        var response = await Client.GetAsync($"ContractMilestone/{_contractMilestone.Id}");

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var milestoneFromResponse = await JsonHelper.GetPayloadAsync<ContractMilestoneVM>(response);

        milestoneFromResponse.Should().NotBeNull();
        milestoneFromResponse.Id.Should().Be(_contractMilestone.Id);
        milestoneFromResponse.ContractId.Should().Be(_contract.Id);
    }

    [Fact]
    public async Task ShouldGetContractMilestonesByContractId()
    {
        // Act
        var response = await Client.GetAsync($"ContractMilestone/by-contract/{_contract.Id}");

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var milestones = await JsonHelper.GetPayloadAsync<List<ContractMilestoneVM>>(response);

        milestones.Should().NotBeEmpty();
        milestones.Should().Contain(m => m.Id == _contractMilestone.Id);
    }

    [Fact]
    public async Task ShouldNotCreateContractMilestoneBecauseContractNotFound()
    {
        // Arrange
        var request = new CreateContractMilestoneVM
        {
            ContractId = Guid.NewGuid(),
            Description = "Test",
            Amount = 500m,
            DueDate = DateTime.UtcNow.AddDays(30)
        };

        // Act
        var response = await Client.PostAsJsonAsync("ContractMilestone", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldNotUpdateBecauseNotFound()
    {
        // Arrange
        var request = new UpdateContractMilestoneVM
        {
            Description = "Test",
            Amount = 500m,
            DueDate = DateTime.UtcNow.AddDays(30)
        };

        // Act
        var response = await Client.PutAsJsonAsync($"ContractMilestone/{Guid.NewGuid()}", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldNotDeleteBecauseNotFound()
    {
        // Act
        var response = await Client.DeleteAsync($"ContractMilestone/{Guid.NewGuid()}");

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldNotGetByIdBecauseNotFound()
    {
        // Act
        var response = await Client.GetAsync($"ContractMilestone/{Guid.NewGuid()}");

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldReturnEmptyListForContractWithNoMilestones()
    {
        // Arrange
        var contractWithoutMilestones = ContractData.CreateContract(
            projectId: _project.Id,
            freelancerId: _freelancerUser.Id
        );
        await Context.AddAsync(contractWithoutMilestones);
        await SaveChangesAsync();

        // Act
        var response = await Client.GetAsync($"ContractMilestone/by-contract/{contractWithoutMilestones.Id}");

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var milestones = await JsonHelper.GetPayloadAsync<List<ContractMilestoneVM>>(response);

        milestones.Should().BeEmpty();
    }

    [Fact]
    public async Task ShouldNotCreateContractMilestone_WhenAmountExceedsContractBudget()
    {
        // Arrange
        var contract =
            ContractData.CreateContract(agreedRate: 1000m, projectId: _project.Id,
                freelancerId: _freelancerUser.Id, createdById: UserId);
        await Context.AddAsync(contract);
        await SaveChangesAsync();

        var firstMilestone = new CreateContractMilestoneVM
        {
            ContractId = contract.Id,
            Description = "First contract milestone",
            Amount = 900m,
            DueDate = DateTime.UtcNow.AddDays(10)
        };
        var response1 = await Client.PostAsJsonAsync("ContractMilestone", firstMilestone);
        response1.IsSuccessStatusCode.Should().BeTrue();

        var secondMilestone = new CreateContractMilestoneVM
        {
            ContractId = contract.Id,
            Description = "Second contract milestone",
            Amount = 200m,
            DueDate = DateTime.UtcNow.AddDays(20)
        };
        // Act
        var response2 = await Client.PostAsJsonAsync("ContractMilestone", secondMilestone);

        // Assert
        response2.IsSuccessStatusCode.Should().BeFalse();
        response2.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldNotUpdateContractMilestone_WhenAmountExceedsContractBudget()
    {
        // Arrange
        var contract =
            ContractData.CreateContract(agreedRate: 1000m,
                projectId: _project.Id, freelancerId: _freelancerUser.Id, createdById: UserId);
        await Context.AddAsync(contract);
        var milestone = new ContractMilestone
        {
            Id = Guid.NewGuid(),
            ContractId = contract.Id,
            Description = "Milestone",
            Amount = 900m,
            DueDate = DateTime.UtcNow.AddDays(10),
            Status = ContractMilestoneStatus.Pending,
            CreatedBy = UserId
        };
        await Context.AddAsync(milestone);
        await SaveChangesAsync();

        var updateRequest = new UpdateContractMilestoneVM
        {
            Description = "Milestone updated",
            Amount = 1100m, // перевищує бюджет
            DueDate = DateTime.UtcNow.AddDays(20)
        };
        // Act
        var response = await Client.PutAsJsonAsync($"ContractMilestone/{milestone.Id}", updateRequest);

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldUpdateContractStatusToInProgress_WhenFirstMilestoneInProgress()
    {
        // Arrange
        var contract =
            ContractData.CreateContract(projectId: _project.Id, freelancerId: _freelancer.Id, agreedRate: 1000m,
                createdById: UserId);
        await Context.AddAsync(contract);
        var milestone1 = new ContractMilestone
        {
            Id = Guid.NewGuid(), ContractId = contract.Id, Description = "M1", Amount = 500m,
            DueDate = DateTime.UtcNow.AddDays(1), Status = ContractMilestoneStatus.Pending,
            CreatedBy = UserId
        };
        await Context.AddAsync(milestone1);
        await SaveChangesAsync();

        // Act: Set milestone to InProgress (фрілансер)
        var inProgressVm = new UpdContractMilestoneStatusFreelancerVM
            { Status = ContractMilestoneFreelancerStatus.InProgress };
        await Client.PutAsJsonAsync($"ContractMilestone/status/{milestone1.Id}/freelancer", inProgressVm);

        // Assert
        var contractFromDb = await Context.Set<Contract>().FirstOrDefaultAsync(x => x.Id == contract.Id);
        contractFromDb.Should().NotBeNull();
        contractFromDb.Status.Should().Be(ContractStatus.InProgress);
    }

    public async Task InitializeAsync()
    {
        _freelancerUser = UserData.CreateTestUser(UserId);
        _freelancer = FreelancerData.CreateFreelancer(userId: _freelancerUser.Id);
        _project = ProjectData.CreateProject();
        _contract = ContractData.CreateContract(
            projectId: _project.Id,
            freelancerId: _freelancer.Id,
            agreedRate: 1000m,
            createdById: UserId
        );
        _contractMilestone = new ContractMilestone
        {
            Id = Guid.NewGuid(),
            ContractId = _contract.Id,
            Description = "Test contract milestone",
            Amount = _contract.AgreedRate / 2,
            DueDate = DateTime.UtcNow.AddDays(30),
            Status = ContractMilestoneStatus.Pending,
            CreatedBy = UserId
        };

        await Context.AddAsync(_freelancerUser);
        await Context.AddAsync(_freelancer);
        await Context.AddAsync(_project);
        await Context.AddAsync(_contract);
        await Context.AddAsync(_contractMilestone);
        await SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        Context.Set<ContractMilestone>().RemoveRange(Context.Set<ContractMilestone>());
        Context.Set<Contract>().RemoveRange(Context.Set<Contract>());
        Context.Set<Project>().RemoveRange(Context.Set<Project>());
        Context.Set<UserWallet>().RemoveRange(Context.Set<UserWallet>());
        Context.Set<Freelancer>().RemoveRange(Context.Set<Freelancer>());
        Context.Set<User>().RemoveRange(Context.Set<User>());
        await SaveChangesAsync();
    }
}