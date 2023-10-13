using Common.Factories;
using Northwind.Application.Products.Commands.UpdateProduct;
using Northwind.Infrastructure.Persistence;
using Northwind.WebUI.IntegrationTests.Common;
using System.Net;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.WebUI.IntegrationTests.Controllers.Products;

[Collection(WebUICollection.Definition)]
public class Update
{
    private readonly CustomWebApplicationFactory _factory;

    public Update(CustomWebApplicationFactory factory, ITestOutputHelper output)
    {
        _factory = factory;
        _factory.Output = output;
    }

    [Fact]
    public async Task GivenUpdateProductCommand_ReturnsSuccessStatusCode()
    {
        // Arrange
        var client = await _factory.GetAuthenticatedClientAsync();
        var product = ProductFactory.Generate();
        await _factory.AddEntityAsync(product);

        var command = new UpdateProductCommand
        (
            product.Id.Value,
            "Chai",
            15.00m,
            1,
            1,
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
        var client = await _factory.GetAuthenticatedClientAsync();
        var invalidCommand = new UpdateProductCommand
        (
            0,
            "Original Frankfurter grüne Soße",
            15.00m,
            1,
            2,
            false
        );
        var content = Utilities.GetRequestContent(invalidCommand);

        // Act
        var response = await client.PutAsync($"/api/products", content);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}