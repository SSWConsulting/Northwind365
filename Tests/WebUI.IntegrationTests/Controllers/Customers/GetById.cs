using Common.Factories;
using FluentAssertions;
using Northwind.Application.Common.Interfaces;
using Northwind.Application.Customers.Queries.GetCustomerDetail;
using Northwind.WebUI.IntegrationTests.Common;
using System.Net;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.WebUI.IntegrationTests.Controllers.Customers;

[Collection(WebUICollection.Definition)]
public class GetById
{
    private readonly CustomWebApplicationFactory _factory;

    public GetById(CustomWebApplicationFactory factory, ITestOutputHelper output)
    {
        _factory = factory;
        _factory.Output = output;
    }

    [Fact]
    public async Task GivenId_ReturnsCustomerViewModel()
    {
        // Arrange
        var client = await _factory.GetAuthenticatedClientAsync();
        var customer = CustomerFactory.Generate();
        await _factory.AddEntityAsync(customer);

        // Act
        var response = await client.GetAsync($"/api/customers/{customer.Id.Value}");

        // Assert
        response.EnsureSuccessStatusCode();

        var customerResp = await Utilities.GetResponseContent<CustomerDetailVm>(response);

        customerResp.Id.Should().Be(customer.Id.Value);
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
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}