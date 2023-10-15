using FluentAssertions;
using Northwind.Application.Products.Commands.CreateProduct;
using Northwind.WebUI.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.WebUI.IntegrationTests.Controllers.Products;

//[Collection(WebUICollection.Definition)]
public class Create : IntegrationTestBase
{
    public Create(TestingDatabaseFixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
    public async Task GivenCreateProductCommand_ReturnsNewProductId()
    {
        // Arrange
        var client = await GetAuthenticatedClientAsync();
        var supplier = Context.Suppliers.First();
        var category = Context.Categories.First();

        var command = new CreateProductCommand
        (
            "Coffee",
            19.00m,
            supplier.Id.Value,
            category.Id.Value,
            false
        );

        var content = Utilities.GetRequestContent(command);

        // Act
        var response = await client.PostAsync($"/api/products", content);

        // Assert
        response.EnsureSuccessStatusCode();

        var productId = await Utilities.GetResponseContent<int>(response);

        productId.Should().NotBe(0);
    }
}