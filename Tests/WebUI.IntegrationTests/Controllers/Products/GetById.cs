using Common.Factories;
using Common.Fixtures;
using FluentAssertions;
using Northwind.Application.Products.Queries.GetProductDetail;
using Northwind.WebUI.IntegrationTests.Common;
using System.Net;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.WebUI.IntegrationTests.Controllers.Products;

public class GetById : IntegrationTestBase
{
    public GetById(TestingDatabaseFixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
    public async Task GivenId_ReturnsProductViewModel()
    {
        var client = await GetAuthenticatedClientAsync();
        var product = ProductFactory.Generate();
        await AddEntityAsync(product);

        var response = await client.GetAsync($"/api/products/{product.Id.Value}");

        response.EnsureSuccessStatusCode();

        var vm = await Utilities.GetResponseContent<ProductDetailVm>(response);

        vm.Should().NotBeNull();
        vm.ProductId.Should().Be(product.Id.Value);
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