using System.Threading.Tasks;
using Northwind.Application.Products.Queries.GetProductsList;
using Northwind.WebUI.IntegrationTests.Common;
using Xunit;

namespace Northwind.WebUI.IntegrationTests.Controllers.Products;

public class GetAll : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;

    public GetAll(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ReturnsProductsListViewModel()
    {
        var client = await _factory.GetAuthenticatedClientAsync();

        var response = await client.GetAsync("/api/products");

        response.EnsureSuccessStatusCode();

        var vm = await Utilities.GetResponseContent<ProductsListVm>(response);

        Assert.IsType<ProductsListVm>(vm);
        Assert.NotEmpty(vm.Products);
    }
}