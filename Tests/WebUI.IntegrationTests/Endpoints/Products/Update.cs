using Common.Factories;
using Common.Fixtures;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Products.Commands.UpdateProduct;
using System.Net;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.WebUI.IntegrationTests.Endpoints.Products;

public class Update(TestingDatabaseFixture fixture, ITestOutputHelper output) : IntegrationTestBase(fixture, output)
{
    [Fact]
    public async Task GivenUpdateProductCommand_ReturnsSuccessStatusCode()
    {
        // Arrange
        var client = await GetAuthenticatedClientAsync();
        var product = ProductFactory.Generate();
        await AddEntityAsync(product);
        var supplier = await Context.Suppliers.FirstAsync();
        var category = await Context.Categories.FirstAsync();

        var command = new UpdateProductCommand
        (
            product.Id.Value,
            "Chai",
            15.00m,
            supplier.Id.Value,
            category.Id.Value,
            false
        );

        // Act
        var response = await client.PutAsJsonAsync($"/api/products", command);

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GivenUpdateProductCommandWithInvalidId_ReturnsNotFoundStatusCode()
    {
        // Arrange
        var client = await GetAuthenticatedClientAsync();
        var supplier = await Context.Suppliers.FirstAsync();
        var category = await Context.Categories.FirstAsync();
        var invalidCommand = new UpdateProductCommand
        (
            0,
            "Original Frankfurter grüne Soße",
            15.00m,
            supplier.Id.Value,
            category.Id.Value,
            false
        );

        // Act
        var response = await client.PutAsJsonAsync($"/api/products", invalidCommand);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}