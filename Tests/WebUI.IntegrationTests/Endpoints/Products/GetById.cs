using Common.Factories;
using Common.Fixtures;
using FluentAssertions;
using Northwind.Application.Products.Queries.GetProductDetail;
using System.Net;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.WebUI.IntegrationTests.Endpoints.Products;

public class GetById(TestingDatabaseFixture fixture, ITestOutputHelper output) : IntegrationTestBase(fixture, output)
{
    [Fact]
    public async Task GivenId_ReturnsProductViewModel()
    {
        // Arrange
        var client = await GetAuthenticatedClientAsync();
        var product = ProductFactory.Generate();
        await AddEntityAsync(product);

        // Act
        var vm = await client.GetFromJsonAsync<ProductDetailVm>($"/api/products/{product.Id.Value}");

        // Assert
        vm.Should().NotBeNull();
        vm!.ProductId.Should().Be(product.Id.Value);
    }

    [Fact]
    public async Task GivenInvalidId_ReturnsNotFoundStatusCode()
    {
        var client = await GetAuthenticatedClientAsync();

        var invalidId = 0;

        var response = await client.GetAsync($"/api/products/{invalidId}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}