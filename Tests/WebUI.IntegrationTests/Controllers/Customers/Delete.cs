using Common.Factories;
using FluentAssertions;
using Northwind.Application.Common.Interfaces;
using Northwind.WebUI.IntegrationTests.Common;
using System.Net;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.WebUI.IntegrationTests.Controllers.Customers;

//[Collection(WebUICollection.Definition)]
public class Delete : IntegrationTestBase
{
    public Delete(TestingDatabaseFixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

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

        var invalidId = "AAAAA";

        // Act
        var response = await client.DeleteAsync($"/api/customers/{invalidId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}