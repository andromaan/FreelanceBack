using System.Net;
using System.Net.Http.Json;
using BLL.ViewModels.Country;
using Domain.Models.Countries;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Tests.Common;
using TestsData;

namespace Api.Tests.Integration;

public class CountryControllerTests(IntegrationTestWebFactory factory)
    : BaseIntegrationTest(factory), IAsyncLifetime
{
    private readonly Country _country = CountryData.MainCountry;

    [Fact]
    public async Task ShouldCreateCountry()
    {
        // Arrange
        var countryName = "TestCountry";
        var alpha2Code = "TC";
        var alpha3Code = "TST";
        var request = new CreateCountryVM { Name = countryName, Alpha2Code = alpha2Code, Alpha3Code = alpha3Code };

        // Act
        var response = await Client.PostAsJsonAsync("Country", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var countryFromResponse = await JsonHelper.GetPayloadAsync<CountryVM>(response);
        var countryId = countryFromResponse.Id;

        var countryFromDb = await Context.Set<Country>().FirstOrDefaultAsync(x => x.Id == countryId);

        countryFromDb.Should().NotBeNull();
        countryFromDb.Name.Should().Be(countryName);
        countryFromDb.Alpha2Code.Should().Be(alpha2Code);
        countryFromDb.Alpha3Code.Should().Be(alpha3Code);
    }
    
    [Fact]
    public async Task ShouldUpdateCountry()
    {
        // Arrange
        var countryName = "UpdatedCountry";
        var alpha2Code = "UC";
        var alpha3Code = "UPD";
        var request = new UpdateCountryVM { Name = countryName, Alpha2Code = alpha2Code, Alpha3Code = alpha3Code };

        // Act
        var response = await Client.PutAsJsonAsync($"Country/{_country.Id}", request);
        
        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        
        var countryFromResponse = await JsonHelper.GetPayloadAsync<CountryVM>(response);
        var countryId = countryFromResponse.Id;
        
        var countryFromDb = await Context.Set<Country>().FirstOrDefaultAsync(x => x.Id == countryId);
        
        countryFromDb.Should().NotBeNull();
        countryFromDb.Name.Should().Be(countryName);
        countryFromDb.Alpha2Code.Should().Be(alpha2Code);
        countryFromDb.Alpha3Code.Should().Be(alpha3Code);
    }
    
    [Fact]
    public async Task ShouldDeleteCountry()
    {
        // Act
        var response = await Client.DeleteAsync($"Country/{_country.Id}");
        
        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        
        var countryFromDb = await Context.Set<Country>().FirstOrDefaultAsync(x => x.Id == _country.Id);
        
        countryFromDb.Should().BeNull();
    }

    [Fact]
    public async Task ShouldGetCountryById()
    {
        // Act
        var response = await Client.GetAsync($"Country/{_country.Id}");
        
        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        
        var countryFromResponse = await JsonHelper.GetPayloadAsync<CountryVM>(response);
        
        countryFromResponse.Should().NotBeNull();
        countryFromResponse.Id.Should().Be(_country.Id);
        countryFromResponse.Name.Should().Be(_country.Name);
    }

    [Fact]
    public async Task ShouldNotUpdateBecauseNotFound()
    {
        // Arrange
        var request = new UpdateCountryVM { Name = "TestCountry", Alpha2Code = "AA", Alpha3Code = "AAA" };
        
        // Act
        var response = await Client.PutAsJsonAsync($"Country/{int.MaxValue}", request);
        
        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task ShouldNotDeleteBecauseNotFound()
    {
        // Act
        var response = await Client.DeleteAsync($"Country/{int.MaxValue}");
        
        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task ShouldNotGetByIdBecauseNotFound()
    {
        // Act
        var response = await Client.GetAsync($"Country/{int.MaxValue}");
        
        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task ShouldGetAllCountries()
    {
        // Act
        var response = await Client.GetAsync("Country");
        
        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        
        var countries = await JsonHelper.GetPayloadAsync<List<CountryVM>>(response);
        
        countries.Should().NotBeEmpty();
    }

    public async Task InitializeAsync()
    {
        await Context.AddAsync(_country);
        await SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        Context.Set<Country>().RemoveRange(Context.Set<Country>());
        await SaveChangesAsync();
    }
}
