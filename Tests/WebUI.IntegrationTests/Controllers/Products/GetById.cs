using Northwind.Application.Products.Queries.GetProductDetail;
using Northwind.WebUI.IntegrationTests.Common;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.WebUI.IntegrationTests.Controllers.Products;

// TODO DM: Make this standard for all tests
[Collection(WebUICollection.Definition)]
public class GetById
{
    private readonly CustomWebApplicationFactoryV2 _factory;

    public GetById(CustomWebApplicationFactoryV2 factory, ITestOutputHelper output)
    {
        _factory = factory;
        _factory.Output = output;
    }

    [Fact]
    public async Task GivenId_ReturnsProductViewModel()
    {
        var client = await _factory.GetAuthenticatedClientAsync();

        var id = 1;

        var response = await client.GetAsync($"/api/products/{id}");

        response.EnsureSuccessStatusCode();

        var product = await Utilities.GetResponseContent<ProductDetailVm>(response);

        Assert.Equal(id, product.ProductId);
    }

    [Fact]
    public async Task GivenInvalidId_ReturnsNotFoundStatusCode()
    {
        var client = await _factory.GetAuthenticatedClientAsync();

        var invalidId = 0;

        var response = await client.GetAsync($"/api/products/{invalidId}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}