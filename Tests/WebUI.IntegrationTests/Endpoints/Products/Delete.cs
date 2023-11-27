using Common.Factories;
using Common.Fixtures;
using FluentAssertions;
using System.Net;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.WebUI.IntegrationTests.Endpoints.Products;

public class Delete(TestingDatabaseFixture fixture, ITestOutputHelper output) : IntegrationTestBase(fixture, output)
{
    [Fact]
    public async Task GivenId_ReturnsSuccessStatusCode()
    {
        // Arrange
        var client = await GetAuthenticatedClientAsync();
        var product = ProductFactory.Generate();
        await AddEntityAsync(product);

        // Act
        var response = await client.DeleteAsync($"/api/products/{product.Id.Value}");

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GivenInvalidId_ReturnsNotFoundStatusCode()
    {
        // Arrange
        var client = await GetAuthenticatedClientAsync();

        var invalidId = 0;

        // Act
        var response = await client.DeleteAsync($"/api/products/{invalidId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}