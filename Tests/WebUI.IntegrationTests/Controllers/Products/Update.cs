using Common.Factories;
using Northwind.Application.Products.Commands.UpdateProduct;
using Northwind.Infrastructure.Persistence;
using Northwind.WebUI.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.WebUI.IntegrationTests.Controllers.Products;

[Collection(WebUICollection.Definition)]
public class Update
{
    private readonly CustomWebApplicationFactory _factory;

    public Update(CustomWebApplicationFactory factory, ITestOutputHelper output)
    {
        _factory = factory;
        _factory.Output = output;
    }

    [Fact]
    public async Task GivenUpdateProductCommand_ReturnsSuccessStatusCode()
    {
        var client = await _factory.GetAuthenticatedClientAsync();

        var product = ProductFactory.Generate();
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<NorthwindDbContext>();
        db.Products.Add(product);
        await db.SaveChangesAsync();

        var command = new UpdateProductCommand
        (
            product.Id.Value,
            "Chai",
            15.00m,
            1,
            1,
            false
        );

        var content = Utilities.GetRequestContent(command);

        var response = await client.PutAsync($"/api/products", content);

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