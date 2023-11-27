using Common.Factories;
using Common.Fixtures;
using FluentAssertions;
using Northwind.Application.Customers.Commands.UpdateCustomer;
using System.Net;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.WebUI.IntegrationTests.Endpoints.Customers;

public class Update(TestingDatabaseFixture fixture, ITestOutputHelper output) : IntegrationTestBase(fixture, output)
{
    [Fact]
    public async Task GivenUpdateCustomerCommand_ReturnsSuccessStatusCode()
    {
        // Arrange
        var client = await GetAuthenticatedClientAsync();

        var customer = CustomerFactory.Generate();
        await AddEntityAsync(customer);

        var command = new UpdateCustomerCommand
        (
            customer.Id.Value,
            "Obere Str. 57",
            "Berlin",
            "Alfreds Futterkiste",
            "Maria Anders",
            "Sales Representative",
            "Germany",
            "030-0076545",
            "030-0074321",
            "12209",
            "Region"
        );

        // Act
        var response = await client.PutAsJsonAsync($"/api/customers/{command.Id}", command);

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GivenUpdateCustomerCommandWithInvalidId_ReturnsNotFoundStatusCode()
    {
        // Arrange
        var client = await GetAuthenticatedClientAsync();

        var invalidCommand = new UpdateCustomerCommand
        (
            "XXX",
            "Obere Str. 57",
            "Berlin",
            "Alfreds Futterkiste",
            "Maria Anders",
            "Sales Representative",
            "Germany",
            "030-0076545",
            "030-0074321",
            "12209",
            "Region"
        );

        // Act
        var response = await client.PutAsJsonAsync($"/api/customers/{invalidCommand.Id}", invalidCommand);

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}