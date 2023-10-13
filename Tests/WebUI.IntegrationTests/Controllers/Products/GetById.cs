using Northwind.Application.Products.Queries.GetProductDetail;
using Northwind.WebUI.IntegrationTests.Common;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Northwind.WebUI.IntegrationTests.Controllers.Products;

[Collection(WebUICollection.Definition)]
public class GetById
{
    private readonly CustomWebApplicationFactoryV2 _factory;

    public GetById(CustomWebApplicationFactoryV2 factory)
    {
        _factory = factory;
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