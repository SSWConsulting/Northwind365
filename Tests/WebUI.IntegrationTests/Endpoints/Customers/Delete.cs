using Common.Factories;
using Common.Fixtures;
using FluentAssertions;
using System.Net;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.WebUI.IntegrationTests.Endpoints.Customers;

public class Delete(TestingDatabaseFixture fixture, ITestOutputHelper output) : IntegrationTestBase(fixture, output)
{
    [Fact]
    public async Task GivenId_ReturnsSuccessStatusCode()
    {
        // Arrange
        var customer = CustomerFactory.Generate();
        await AddEntityAsync(customer);

        var client = await GetAuthenticatedClientAsync();
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
        var client = await GetAuthenticatedClientAsync();

        var invalidId = "XXX";

        // Act
        var response = await client.DeleteAsync($"/api/customers/{invalidId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}