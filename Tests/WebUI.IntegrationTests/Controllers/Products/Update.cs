using System.Net;
using System.Threading.Tasks;
using Northwind.Application.Products.Commands.UpdateProduct;
using Northwind.Domain.Supplying;
using Northwind.WebUI.IntegrationTests.Common;
using Xunit;

namespace Northwind.WebUI.IntegrationTests.Controllers.Products;

public class Update : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public Update(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GivenUpdateProductCommand_ReturnsSuccessStatusCode()
    {
        var client = await _factory.GetAuthenticatedClientAsync();

        var command = new UpdateProductCommand
        (
            1,
            "Chai",
            15.00m,
            1,
            1,
            false
        );

        var content = Utilities.GetRequestContent(command);

        var response = await client.PutAsync($"/api/products/update", content);

        response.EnsureSuccessStatusCode();
    }

    // TODO: Add back in
    // [Fact]
    // public async Task GivenUpdateProductCommandWithInvalidId_ReturnsNotFoundStatusCode()
    // {
    //     var client = await _factory.GetAuthenticatedClientAsync();
    //
    //     var invalidCommand = new UpdateProductCommand
    //     {
    //         ProductId = 0,
    //         ProductName = "Original Frankfurter grüne Soße",
    //         SupplierId = 1,
    //         CategoryId = 2,
    //         UnitPrice = 15.00m,
    //         Discontinued = false
    //     };
    //
    //     var content = Utilities.GetRequestContent(invalidCommand);
    //
    //     var response = await client.PutAsync($"/api/products/update", content);
    //
    //     Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    // }
}