using Northwind.Application.Products.Commands.CreateProduct;
using Northwind.WebUI.IntegrationTests.Common;
using System.Threading.Tasks;
using Northwind.Domain.Supplying;
using Xunit;

namespace Northwind.WebUI.IntegrationTests.Controllers.Products;

public class Create : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;

    public Create(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GivenCreateProductCommand_ReturnsNewProductId()
    {
        var client = await _factory.GetAuthenticatedClientAsync();

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