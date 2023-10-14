using Northwind.Application.Products.Commands.CreateProduct;
using Northwind.WebUI.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.WebUI.IntegrationTests.Controllers.Products;

[Collection(WebUICollection.Definition)]
public class Create
{
    private readonly CustomWebApplicationFactory _fixture;

    public Create(CustomWebApplicationFactory fixture, ITestOutputHelper output)
    {
        _fixture = fixture;
        _fixture.Output = output;
    }

    [Fact]
    public async Task GivenCreateProductCommand_ReturnsNewProductId()
    {
        var client = await _fixture.GetAuthenticatedClientAsync();

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

        Assert.NotEqual(0, productId);
    }
}