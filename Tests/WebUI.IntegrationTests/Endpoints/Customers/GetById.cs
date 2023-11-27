using Common.Factories;
using Common.Fixtures;
using FluentAssertions;
using Northwind.Application.Customers.Queries.GetCustomerDetail;
using System.Net;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.WebUI.IntegrationTests.Endpoints.Customers;

public class GetById(TestingDatabaseFixture fixture, ITestOutputHelper output) : IntegrationTestBase(fixture, output)
{
    [Fact]
    public async Task GivenId_ReturnsCustomerViewModel()
    {
        // Arrange
        var client = await GetAuthenticatedClientAsync();
        var customer = CustomerFactory.Generate();
        await AddEntityAsync(customer);

        // Act
        var customerResp = await client.GetFromJsonAsync<CustomerDetailVm>($"/api/customers/{customer.Id.Value}");

        // Assert
        customerResp.Should().NotBeNull();
        customerResp!.Id.Should().Be(customer.Id.Value);
    }

    [Fact]
    public async Task GivenInvalidId_ReturnsNotFoundStatusCode()
    {
        // Arrange
        var client = await GetAuthenticatedClientAsync();
        
        var invalidId = "XXX";

        // Act
        var response = await client.GetAsync($"/api/customers/{invalidId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}