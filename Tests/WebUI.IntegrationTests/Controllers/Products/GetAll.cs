using Common.Factories;
using Common.Fixtures;
using FluentAssertions;
using Northwind.Application.Products.Queries.GetProductsList;
using Northwind.WebUI.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.WebUI.IntegrationTests.Controllers.Products;

public class GetAll : IntegrationTestBase
{
    public GetAll(TestingDatabaseFixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
    public async Task ReturnsProductsListViewModel()
    {
        var client = await GetAuthenticatedClientAsync();
        var product = ProductFactory.Generate();
        await AddEntityAsync(product);

        var response = await client.GetAsync("/api/products");

        response.EnsureSuccessStatusCode();

        var vm = await Utilities.GetResponseContent<ProductsListVm>(response);

        vm.Should().BeOfType<ProductsListVm>();
        vm.Products.Should().NotBeEmpty();
    }
}