using Common.Factories;
using Common.Fixtures;
using FluentAssertions;
using Northwind.Application.Products.Commands.UpdateProduct;
using Northwind.WebUI.IntegrationTests.Common;
using System.Net;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.WebUI.IntegrationTests.Controllers.Products;

public class Update : IntegrationTestBase
{
    public Update(TestingDatabaseFixture fixture, ITestOutputHelper output): base(fixture, output)
    {
    }

    [Fact]
    public async Task GivenUpdateProductCommand_ReturnsSuccessStatusCode()
    {
        // Arrange
        var client = await GetAuthenticatedClientAsync();
        var product = ProductFactory.Generate();
        await AddEntityAsync(product);
        var supplier = Context.Suppliers.First();
        var category = Context.Categories.First();

        var command = new UpdateProductCommand
        (
            product.Id.Value,
            "Chai",
            15.00m,
            supplier.Id.Value,
            category.Id.Value,
            false
        );

        var content = Utilities.GetRequestContent(command);

        // Act
        var response = await client.PutAsync($"/api/products", content);

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GivenUpdateProductCommandWithInvalidId_ReturnsNotFoundStatusCode()
    {
        // Arrange
        var client = await GetAuthenticatedClientAsync();
        var supplier = Context.Suppliers.First();
        var category = Context.Categories.First();
        var invalidCommand = new UpdateProductCommand
        (
            0,
            "Original Frankfurter grüne Soße",
            15.00m,
            supplier.Id.Value,
            category.Id.Value,
            false
        );
        var content = Utilities.GetRequestContent(invalidCommand);

        // Act
        var response = await client.PutAsync($"/api/products", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}