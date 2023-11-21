using Common.Fixtures;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Products.Commands.CreateProduct;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.WebUI.IntegrationTests.Endpoints.Products;

public class Create(TestingDatabaseFixture fixture, ITestOutputHelper output) : IntegrationTestBase(fixture, output)
{
    [Fact]
    public async Task GivenCreateProductCommand_ReturnsNewProductId()
    {
        // Arrange
        var client = await GetAuthenticatedClientAsync();
        var supplier = await Context.Suppliers.FirstAsync();
        var category = await Context.Categories.FirstAsync();

        var command = new CreateProductCommand
        (
            "Coffee",
            19.00m,
            supplier.Id.Value,
            category.Id.Value,
            false
        );

        // Act
        var response = await client.PostAsJsonAsync($"/api/products", command);

        // Assert
        response.EnsureSuccessStatusCode();
        var productId = await response.Content.ReadFromJsonAsync<int>();
        productId.Should().NotBe(0);
    }

    [Fact]
    public async Task CreateProductCommand_WithNoSupplier_CreatesProduct()
    {
        // Arrange
        var client = await GetAuthenticatedClientAsync();
        int? supplier = null;
        var category = await Context.Categories.FirstAsync();

        var command = new CreateProductCommand
        (
            "Coffee",
            19.00m,
            supplier,
            category.Id.Value,
            false
        );

        // Act
        var response = await client.PostAsJsonAsync($"/api/products", command);

        // Assert
        response.EnsureSuccessStatusCode();
        var productId = await response.Content.ReadFromJsonAsync<int>();
        productId.Should().NotBe(0);
    }

    [Fact]
    public async Task CreateProductCommand_WithNoCategory_CreatesProduct()
    {
        // Arrange
        var client = await GetAuthenticatedClientAsync();
        var supplier = await Context.Suppliers.FirstAsync();
        int? category = null;

        var command = new CreateProductCommand
        (
            "Coffee",
            19.00m,
            supplier.Id.Value,
            category,
            false
        );

        // Act
        var response = await client.PostAsJsonAsync($"/api/products", command);

        // Assert
        response.EnsureSuccessStatusCode();
        var productId = await response.Content.ReadFromJsonAsync<int>();
        productId.Should().NotBe(0);
    }

    [Fact]
    public async Task CreateProductCommand_WithNoUnitPrice_CreatesProduct()
    {
        // Arrange
        var client = await GetAuthenticatedClientAsync();
        var supplier = await Context.Suppliers.FirstAsync();
        var category = await Context.Categories.FirstAsync();
        decimal? unitPrice = null;

        var command = new CreateProductCommand
        (
            "Coffee",
            unitPrice,
            supplier.Id.Value,
            category.Id.Value,
            false
        );

        // Act
        var response = await client.PostAsJsonAsync($"/api/products", command);

        // Assert
        response.EnsureSuccessStatusCode();
        var productId = await response.Content.ReadFromJsonAsync<int>();
        productId.Should().NotBe(0);
    }
}