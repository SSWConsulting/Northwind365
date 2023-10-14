using FluentAssertions;
using System.Threading.Tasks;
using Northwind.Application.Products.Queries.GetProductsList;
using Northwind.WebUI.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.WebUI.IntegrationTests.Controllers.Products;

[Collection(WebUICollection.Definition)]
public class GetAll
{
    private readonly CustomWebApplicationFactory _factory;

    public GetAll(CustomWebApplicationFactory factory, ITestOutputHelper output)
    {
        _factory = factory;
        _factory.Output = output;
    }

    [Fact]
    public async Task ReturnsProductsListViewModel()
    {
        var client = await _factory.GetAuthenticatedClientAsync();

        var response = await client.GetAsync("/api/products");

        response.EnsureSuccessStatusCode();

        var vm = await Utilities.GetResponseContent<ProductsListVm>(response);

        vm.Should().BeOfType<ProductsListVm>();
        vm.Products.Should().NotBeEmpty();
    }
}