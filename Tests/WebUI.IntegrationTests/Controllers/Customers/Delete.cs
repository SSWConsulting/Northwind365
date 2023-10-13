using Common.Factories;
using Northwind.Application.Common.Interfaces;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Northwind.WebUI.IntegrationTests.Common;
using Xunit;

namespace Northwind.WebUI.IntegrationTests.Controllers.Customers;

public class Delete : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;

    public Delete(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GivenId_ReturnsSuccessStatusCode()
    {
        // Arrange
        var customer = CustomerFactory.Generate();
        var dbContext = _factory.Services.GetRequiredService<INorthwindDbContext>();
        dbContext.Customers.Add(customer);
        await dbContext.SaveChangesAsync(CancellationToken.None);

        var client = await _factory.GetAuthenticatedClientAsync();
        var validId = customer.Id.Value;

        // Act
        var response = await client.DeleteAsync($"/api/customers/{validId}");

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GivenInvalidId_ReturnsNotFoundStatusCode()
    {
        // Arrange
        var client = await _factory.GetAuthenticatedClientAsync();

        var invalidId = "AAAAA";

        // Act
        var response = await client.DeleteAsync($"/api/customers/{invalidId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}