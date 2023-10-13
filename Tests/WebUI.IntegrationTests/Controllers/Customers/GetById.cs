using Common.Factories;
using Northwind.Application.Common.Interfaces;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Northwind.Application.Customers.Queries.GetCustomerDetail;
using Northwind.WebUI.IntegrationTests.Common;
using Xunit;

namespace Northwind.WebUI.IntegrationTests.Controllers.Customers;

public class GetById : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;

    public GetById(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GivenId_ReturnsCustomerViewModel()
    {
        // Arrange
        var client = await _factory.GetAuthenticatedClientAsync();
        var customer = CustomerFactory.Generate();
        var dbContext = _factory.Services.GetRequiredService<INorthwindDbContext>();
        dbContext.Customers.Add(customer);
        await dbContext.SaveChangesAsync(CancellationToken.None);

        // Act
        var response = await client.GetAsync($"/api/customers/{customer.Id.Value}");

        // Assert
        response.EnsureSuccessStatusCode();

        var customerResp = await Utilities.GetResponseContent<CustomerDetailVm>(response);

        Assert.Equal(customer.Id.Value, customerResp.Id);
    }

    [Fact]
    public async Task GivenInvalidId_ReturnsNotFoundStatusCode()
    {
        // Arrange
        var client = await _factory.GetAuthenticatedClientAsync();
        
        var invalidId = "AAAAA";

        // Act
        var response = await client.GetAsync($"/api/customers/{invalidId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}