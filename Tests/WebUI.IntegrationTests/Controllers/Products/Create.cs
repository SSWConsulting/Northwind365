using FluentAssertions;
using Northwind.Application.Products.Commands.CreateProduct;
using Northwind.WebUI.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.WebUI.IntegrationTests.Controllers.Products;

//[Collection(WebUICollection.Definition)]
public class Create : IntegrationTestBase
{
    //private readonly CustomWebApplicationFactory _fixture;

    public Create(TestingDatabaseFixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
    public async Task GivenCreateProductCommand_ReturnsNewProductId()
    {
        var client = await GetAuthenticatedClientAsync();

        var command = new CreateProductCommand
        (
            "Coffee",
            19.00m,
            1,
            1,
            false
        );

        var content = Utilities.GetRequestContent(command);

        var response = await client.PostAsync($"/api/products", content);

        response.EnsureSuccessStatusCode();

        var productId = await Utilities.GetResponseContent<int>(response);

        productId.Should().NotBe(0);
    }
}