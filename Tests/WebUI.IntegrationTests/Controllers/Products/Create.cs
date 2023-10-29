using Common.Fixtures;
using FluentAssertions;
using Northwind.Application.Products.Commands.CreateProduct;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.WebUI.IntegrationTests.Controllers.Products;

public class Create : IntegrationTestBase
{
    public Create(TestingDatabaseFixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
    public async Task GivenCreateProductCommand_ReturnsNewProductId()
    {
        // Arrange
        var client = await GetAuthenticatedClientAsync();
        var supplier = Context.Suppliers.First().Id.Value;
        var category = Context.Categories.First().Id.Value;

        var command = new CreateProductCommand
        (
            "Coffee",
            19.00m,
            supplier,
            category,
            false
        );

        // Act
        var response = await client.PostAsJsonAsync($"/api/products", command);

        // Assert
        response.EnsureSuccessStatusCode();
        var productId = response.Content.ReadFromJsonAsync<int>();
        productId.Should().NotBe(0);
    }

    [Fact]
    public async Task CreateProductCommand_WithNoSupplier_CreatesProduct()
    {
        // Arrange
        var client = await GetAuthenticatedClientAsync();
        int? supplier = null;
        var category = Context.Categories.First().Id.Value;

        var command = new CreateProductCommand
        (
            "Coffee",
            19.00m,
            supplier,
            category,
            false
        );

        // Act
        var response = await client.PostAsJsonAsync($"/api/products", command);

        // Assert
        response.EnsureSuccessStatusCode();
        var productId = response.Content.ReadFromJsonAsync<int>();
        productId.Should().NotBe(0);
    }

    [Fact]
    public async Task CreateProductCommand_WithNoCategory_CreatesProduct()
    {
        // Arrange
        var client = await GetAuthenticatedClientAsync();
        var supplier = Context.Suppliers.First().Id.Value;
        int? category = null;

        var command = new CreateProductCommand
        (
            "Coffee",
            19.00m,
            supplier,
            category,
            false
        );

        // Act
        var response = await client.PostAsJsonAsync($"/api/products", command);

        // Assert
        response.EnsureSuccessStatusCode();
        var productId = response.Content.ReadFromJsonAsync<int>();
        productId.Should().NotBe(0);
    }

    [Fact]
    public async Task CreateProductCommand_WithNoUnitPrice_CreatesProduct()
    {
        // Arrange
        var client = await GetAuthenticatedClientAsync();
        var supplier = Context.Suppliers.First().Id.Value;
        var category = Context.Categories.First().Id.Value;
        decimal? unitPrice = null;

        var command = new CreateProductCommand
        (
            "Coffee",
            unitPrice,
            supplier,
            category,
            false
        );

        // Act
        var response = await client.PostAsJsonAsync($"/api/products", command);

        // Assert
        response.EnsureSuccessStatusCode();
        var productId = response.Content.ReadFromJsonAsync<int>();
        productId.Should().NotBe(0);
    }
}