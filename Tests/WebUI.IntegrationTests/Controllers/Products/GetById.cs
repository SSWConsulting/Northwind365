using FluentAssertions;
using Northwind.Application.Products.Queries.GetProductDetail;
using Northwind.WebUI.IntegrationTests.Common;
using System.Net;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.WebUI.IntegrationTests.Controllers.Products;

//[Collection(WebUICollection.Definition)]
public class GetById : IntegrationTestBase
{
    public GetById(TestingDatabaseFixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
    public async Task GivenId_ReturnsProductViewModel()
    {
        var client = await GetAuthenticatedClientAsync();

        var id = 1;

        var response = await client.GetAsync($"/api/products/{id}");

        response.EnsureSuccessStatusCode();

        var product = await Utilities.GetResponseContent<ProductDetailVm>(response);

        product.ProductId.Should().Be(id);
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