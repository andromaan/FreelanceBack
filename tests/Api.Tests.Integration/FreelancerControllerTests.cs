using System.Net;
using System.Net.Http.Json;
using BLL.ViewModels.Freelancer;
using Domain.Models.Countries;
using Domain.Models.Freelance;
using Domain.Models.Languages;
using Domain.Models.Users;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Tests.Common;
using TestsData;

namespace Api.Tests.Integration;

public class FreelancerControllerTests(IntegrationTestWebFactory factory)
    : BaseIntegrationTest(factory), IAsyncLifetime
{
    private Freelancer _freelancer = null!;
    private User _user = null!;
    private Country _country = null!;
    private Language _language1 = null!;
    private Language _language2 = null!;

    [Fact]
    public async Task ShouldGetFreelancerByUser()
    {
        // Act
        var response = await Client.GetAsync("Freelancer");
        
        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        
        var freelancerFromResponse = await JsonHelper.GetPayloadAsync<FreelancerVM>(response);
        
        freelancerFromResponse.Should().NotBeNull();
        freelancerFromResponse.Bio.Should().Be(_freelancer.Bio);
        freelancerFromResponse.HourlyRate.Should().Be(_freelancer.HourlyRate);
        freelancerFromResponse.Location.Should().Be(_freelancer.Location);
    }
    
    [Fact]
    public async Task ShouldUpdateFreelancer()
    {
        // Arrange
        var request = new UpdateFreelancerVM 
        { 
            Bio = "Updated Bio",
            HourlyRate = 75.0m,
            Location = "Updated Location",
            CountryId = _country.Id
        };

        // Act
        var response = await Client.PutAsJsonAsync("Freelancer", request);
        
        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        
        // var freelancerFromResponse = await JsonHelper.GetPayloadAsync<FreelancerVM>(response);
        
        var freelancerFromDb = await Context.Set<Freelancer>()
            .FirstOrDefaultAsync(x => x.CreatedBy == _user.Id);
        
        freelancerFromDb.Should().NotBeNull();
        freelancerFromDb.Bio.Should().Be("Updated Bio");
        freelancerFromDb.HourlyRate.Should().Be(75.0m);
        freelancerFromDb.Location.Should().Be("Updated Location");
        freelancerFromDb.CountryId.Should().Be(_country.Id);
    }
    
    [Fact]
    public async Task ShouldUpdateFreelancerLanguages()
    {
        // Arrange
        var request = new UpdateFreelancerLanguagesVM 
        { 
            LanguageIds = new List<int> { _language1.Id, _language2.Id }
        };

        // Act
        var response = await Client.PutAsJsonAsync("Freelancer/languages", request);
        
        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        
        var freelancerFromDb = await Context.Set<Freelancer>()
            .Include(f => f.Languages)
            .FirstOrDefaultAsync(x => x.CreatedBy == _user.Id);
        
        freelancerFromDb.Should().NotBeNull();
        freelancerFromDb.Languages.Should().HaveCount(2);
        freelancerFromDb.Languages.Should().Contain(l => l.Id == _language1.Id);
        freelancerFromDb.Languages.Should().Contain(l => l.Id == _language2.Id);
    }
    
    [Fact]
    public async Task ShouldValidateHourlyRateIsPositive()
    {
        // Arrange
        var request = new UpdateFreelancerVM 
        { 
            Bio = "Test Bio",
            HourlyRate = -10m,
            Location = "Test Location",
            CountryId = _country.Id
        };

        // Act
        var response = await Client.PutAsJsonAsync("Freelancer", request);
        
        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task ShouldUpdateFreelancerLanguagesWithEmptyList()
    {
        // Arrange
        var request = new UpdateFreelancerLanguagesVM 
        { 
            LanguageIds = new List<int>()
        };

        // Act
        var response = await Client.PutAsJsonAsync("Freelancer/languages", request);
        
        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        
        var freelancerFromDb = await Context.Set<Freelancer>()
            .Include(f => f.Languages)
            .FirstOrDefaultAsync(x => x.CreatedBy == _user.Id);
        
        freelancerFromDb.Should().NotBeNull();
        freelancerFromDb.Languages.Should().BeEmpty();
    }
    
    [Fact]
    public async Task ShouldNotUpdateFreelancerLanguagesBecauseLanguageNotFound()
    {
        // Arrange
        var request = new UpdateFreelancerLanguagesVM 
        { 
            LanguageIds = new List<int> { int.MaxValue }
        };

        // Act
        var response = await Client.PutAsJsonAsync("Freelancer/languages", request);
        
        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    public async Task InitializeAsync()
    {
        _user = UserData.CreateTestUser(UserId);
        _country = CountryData.MainCountry;
        _language1 = new Language { Id = 0, Name = "English", Code = "EN" };
        _language2 = new Language { Id = 0, Name = "Spanish", Code = "ES" };
        _freelancer = FreelancerData.CreateFreelancer(userId: _user.Id);
        _freelancer.CountryId = _country.Id;

        await Context.AddAsync(_user);
        await Context.AddAsync(_country);
        await Context.AddAsync(_language1);
        await Context.AddAsync(_language2);
        await Context.AddAsync(_freelancer);
        await SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        Context.Set<Freelancer>().RemoveRange(Context.Set<Freelancer>());
        Context.Set<Language>().RemoveRange(Context.Set<Language>());
        Context.Set<Country>().RemoveRange(Context.Set<Country>());
        Context.Set<User>().RemoveRange(Context.Set<User>());
        await SaveChangesAsync();
    }
}
