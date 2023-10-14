using Common.Factories;
using FluentAssertions;
using Northwind.Infrastructure.Persistence;
using Northwind.WebUI.IntegrationTests.Common;
using System.Net;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.WebUI.IntegrationTests.Controllers.Products;

[Collection(WebUICollection.Definition)]
public class Delete
{
    private readonly CustomWebApplicationFactory _factory;

    public Delete(CustomWebApplicationFactory factory, ITestOutputHelper output)
    {
        _factory = factory;
        _factory.Output = output;
    }

    [Fact]
    public async Task GivenId_ReturnsSuccessStatusCode()
    {
        // Arrange
        var client = await _factory.GetAuthenticatedClientAsync();
        var product = ProductFactory.Generate();
        await _factory.AddEntityAsync(product);

        // Act
        var response = await client.DeleteAsync($"/api/products/{product.Id.Value}");

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GivenInvalidId_ReturnsNotFoundStatusCode()
    {
        // Arrange
        var client = await _factory.GetAuthenticatedClientAsync();

        var invalidId = 0;

        // Act
        var response = await client.DeleteAsync($"/api/products/{invalidId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}