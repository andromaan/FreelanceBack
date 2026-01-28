using System.Net;
using System.Net.Http.Json;
using BLL.ViewModels.Category;
using Domain.Models.Projects;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Tests.Common;
using TestsData;

namespace Api.Tests.Integration;

public class CategoryControllerTests(IntegrationTestWebFactory factory)
    : BaseIntegrationTest(factory), IAsyncLifetime
{
    private readonly Category _category = CategoryData.MainCategory;

    [Fact]
    public async Task ShouldCreateCategory()
    {
        // Arrange
        var categoryName = "TestCategory";
        var request = new CreateCategoryVM { Name = categoryName };

        // Act
        var response = await Client.PostAsJsonAsync("Category", request);

        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();

        var categoryFromResponse = await JsonHelper.GetPayloadAsync<CategoryVM>(response);
        var categoryId = categoryFromResponse.Id;

        var categoryFromDb = await Context.Set<Category>().FirstOrDefaultAsync(x => x.Id == categoryId);

        categoryFromDb.Should().NotBeNull();
        categoryFromDb.Name.Should().Be(categoryName);
    }
    
    [Fact]
    public async Task ShouldUpdateCategory()
    {
        // Arrange
        var categoryName = "UpdatedCategory";
        var request = new UpdateCategoryVM { Name = categoryName };

        // Act
        var response = await Client.PutAsJsonAsync($"Category/{_category.Id}", request);
        
        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        
        var categoryFromResponse = await JsonHelper.GetPayloadAsync<CategoryVM>(response);
        var categoryId = categoryFromResponse.Id;
        
        var categoryFromDb = await Context.Set<Category>().FirstOrDefaultAsync(x => x.Id == categoryId);
        
        categoryFromDb.Should().NotBeNull();
        categoryFromDb.Name.Should().Be(categoryName);
    }
    
    [Fact]
    public async Task ShouldDeleteCategory()
    {
        // Act
        var response = await Client.DeleteAsync($"Category/{_category.Id}");
        
        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        
        var categoryFromDb = await Context.Set<Category>().FirstOrDefaultAsync(x => x.Id == _category.Id);
        
        categoryFromDb.Should().BeNull();
    }

    [Fact]
    public async Task ShouldGetCategoryById()
    {
        // Act
        var response = await Client.GetAsync($"Category/{_category.Id}");
        
        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        
        var categoryFromResponse = await JsonHelper.GetPayloadAsync<CategoryVM>(response);
        
        categoryFromResponse.Should().NotBeNull();
        categoryFromResponse.Id.Should().Be(_category.Id);
        categoryFromResponse.Name.Should().Be(_category.Name);
    }

    [Fact]
    public async Task ShouldNotUpdateBecauseNotFound()
    {
        // Arrange
        var request = new UpdateCategoryVM { Name = "TestCategory" };
        
        // Act
        var response = await Client.PutAsJsonAsync($"Category/{int.MaxValue}", request);
        
        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task ShouldNotDeleteBecauseNotFound()
    {
        // Act
        var response = await Client.DeleteAsync($"Category/{int.MaxValue}");
        
        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task ShouldNotGetByIdBecauseNotFound()
    {
        // Act
        var response = await Client.GetAsync($"Category/{int.MaxValue}");
        
        // Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task ShouldGetAllCategories()
    {
        // Act
        var response = await Client.GetAsync("Category");
        
        // Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        
        var categories = await JsonHelper.GetPayloadAsync<List<CategoryVM>>(response);
        
        categories.Should().NotBeEmpty();
    }

    public async Task InitializeAsync()
    {
        await Context.AddAsync(_category);
        await SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        Context.Set<Category>().RemoveRange(Context.Set<Category>());
        await SaveChangesAsync();
    }
}
