using System.Net;
using System.Net.Http.Json;
using BLL.ViewModels.Bid;
using Domain.Models.Auth.Users;
using Domain.Models.Freelance;
using Domain.Models.Projects;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Tests.Common;
using TestsData;

namespace Api.Tests.Integration;

public class BidControllerTests(IntegrationTestWebFactory factory)
    : BaseIntegrationTest(factory), IAsyncLifetime
{
    private Bid _bid = null!;
    private Project _project = null!;
    private Freelancer _freelancer = null!;
    private User _user = null!;

    [Fact]
    public async Task ShouldCreateBid()
    {
        // Arrange
        var request = new CreateBidVM
        {
            ProjectId = _project.Id,
            Amount = 1500m,
            Message = "I can do this project"
        };

        // Act
        var response = await Client.PostAsJsonAsync("Bid", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var bidFromResponse = await JsonHelper.GetPayloadAsync<BidVM>(response);
        var bidId = bidFromResponse.Id;

        var bidFromDb = await Context.Set<Bid>().FirstOrDefaultAsync(x => x.Id == bidId);

        bidFromDb.Should().NotBeNull();
        bidFromDb.ProjectId.Should().Be(_project.Id);
        bidFromDb.Amount.Should().Be(1500m);
        bidFromDb.Message.Should().Be("I can do this project");
    }

    [Fact]
    public async Task ShouldUpdateBid()
    {
        // Arrange
        var request = new UpdateBidVM
        {
            Amount = 2500m,
            Message = "Updated bid message"
        };

        // Act
        var response = await Client.PutAsJsonAsync($"Bid/{_bid.Id}", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var bidFromResponse = await JsonHelper.GetPayloadAsync<BidVM>(response);

        var bidFromDb = await Context.Set<Bid>().FirstOrDefaultAsync(x => x.Id == bidFromResponse.Id);

        bidFromDb.Should().NotBeNull();
        bidFromDb.Amount.Should().Be(2500m);
        bidFromDb.Message.Should().Be("Updated bid message");
    }

    [Fact]
    public async Task ShouldDeleteBid()
    {
        // Act
        var response = await Client.DeleteAsync($"Bid/{_bid.Id}");

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var bidFromDb = await Context.Set<Bid>().FirstOrDefaultAsync(x => x.Id == _bid.Id);

        bidFromDb.Should().BeNull();
    }

    [Fact]
    public async Task ShouldGetBidById()
    {
        // Act
        var response = await Client.GetAsync($"Bid/{_bid.Id}");

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var bidFromResponse = await JsonHelper.GetPayloadAsync<BidVM>(response);

        bidFromResponse.Should().NotBeNull();
        bidFromResponse.Id.Should().Be(_bid.Id);
        bidFromResponse.ProjectId.Should().Be(_project.Id);
    }

    [Fact]
    public async Task ShouldGetBidsByProjectId()
    {
        // Act
        var response = await Client.GetAsync($"Bid/by-project/{_project.Id}");

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var bids = await JsonHelper.GetPayloadAsync<List<BidVM>>(response);

        bids.Should().NotBeEmpty();
        bids.Should().Contain(b => b.Id == _bid.Id);
    }

    [Fact]
    public async Task ShouldNotCreateBidBecauseProjectNotFound()
    {
        // Arrange
        var request = new CreateBidVM
        {
            ProjectId = Guid.NewGuid(),
            Amount = 1500m,
            Message = "Test"
        };

        // Act
        var response = await Client.PostAsJsonAsync("Bid", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldNotCreateBidBecauseAmountIsGreaterThanProjectBudget()
    {
        // Arrange
        var request = new CreateBidVM
        {
            ProjectId = _project.Id,
            Amount = 100000m, // Assuming this is greater than the project budget
            Message = "Test"
        };

        // Act
        var response = await Client.PostAsJsonAsync("Bid", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ShouldNotUpdateBecauseNotFound()
    {
        // Arrange
        var request = new UpdateBidVM
        {
            Amount = 1000m,
            Message = "Test"
        };

        // Act
        var response = await Client.PutAsJsonAsync($"Bid/{Guid.NewGuid()}", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldNotDeleteBecauseNotFound()
    {
        // Act
        var response = await Client.DeleteAsync($"Bid/{Guid.NewGuid()}");

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldNotGetByIdBecauseNotFound()
    {
        // Act
        var response = await Client.GetAsync($"Bid/{Guid.NewGuid()}");

        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldReturnEmptyListForProjectWithNoBids()
    {
        // Arrange
        var projectWithoutBids = ProjectData.CreateProject();
        await Context.AddAsync(projectWithoutBids);
        await SaveChangesAsync();

        // Act
        var response = await Client.GetAsync($"Bid/by-project/{projectWithoutBids.Id}");

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var bids = await JsonHelper.GetPayloadAsync<List<BidVM>>(response);

        bids.Should().BeEmpty();
    }

    public async Task InitializeAsync()
    {
        _user = UserData.CreateTestUser(UserId);
        _project = ProjectData.CreateProject();
        _freelancer = FreelancerData.CreateFreelancer(userId: _user.Id);
        _bid = new Bid
        {
            Id = Guid.NewGuid(),
            ProjectId = _project.Id,
            FreelancerId = _freelancer.Id,
            Amount = 1000m,
            Message = "Test bid message"
        };

        await Context.AddAsync(_user);
        await Context.AddAsync(_project);
        await Context.AddAsync(_freelancer);
        await Context.AddAsync(_bid);
        await SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        Context.Set<Bid>().RemoveRange(Context.Set<Bid>());
        Context.Set<Freelancer>().RemoveRange(Context.Set<Freelancer>());
        Context.Set<Project>().RemoveRange(Context.Set<Project>());
        Context.Set<User>().RemoveRange(Context.Set<User>());
        await SaveChangesAsync();
    }
}