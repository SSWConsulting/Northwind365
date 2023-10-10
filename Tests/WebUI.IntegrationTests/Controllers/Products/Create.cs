using Northwind.Application.Products.Commands.CreateProduct;
using Northwind.WebUI.IntegrationTests.Common;
using System.Threading.Tasks;

using Northwind.Domain.Supplying;

using Xunit;

namespace Northwind.WebUI.IntegrationTests.Controllers.Products;

public class Create : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public Create(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GivenCreateProductCommand_ReturnsNewProductId()
    {
        var client = await _factory.GetAuthenticatedClientAsync();

        var command = new CreateProductCommand
        {
            ProductName = "Coffee",
            SupplierId = Guid.NewGuid(),
            CategoryId = 1,
            UnitPrice = 19.00m,
            Discontinued = false
        };

        var content = Utilities.GetRequestContent(command);

        var response = await client.PostAsync($"/api/products/create", content);

        response.EnsureSuccessStatusCode();

        var productId = await Utilities.GetResponseContent<int>(response);

        Assert.NotEqual(0, productId);
    }
}