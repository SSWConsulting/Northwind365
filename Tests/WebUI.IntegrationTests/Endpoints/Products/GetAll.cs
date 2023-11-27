using Common.Factories;
using Common.Fixtures;
using FluentAssertions;
using Northwind.Application.Products.Queries.GetProductsList;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.WebUI.IntegrationTests.Endpoints.Products;

public class GetAll(TestingDatabaseFixture fixture, ITestOutputHelper output) : IntegrationTestBase(fixture, output)
{
    [Fact]
    public async Task ReturnsProductsListViewModel()
    {
        // Arrange
        var client = await GetAuthenticatedClientAsync();
        var product = ProductFactory.Generate();
        await AddEntityAsync(product);

        // Act
        var vm = await client.GetFromJsonAsync<ProductsListVm>("/api/products");

        // Assert
        vm.Should().NotBeNull();
        vm.Should().BeOfType<ProductsListVm>();
        vm!.Products.Should().NotBeEmpty();
    }
}