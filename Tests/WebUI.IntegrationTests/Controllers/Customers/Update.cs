using Common.Factories;
using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Northwind.Application.Customers.Commands.UpdateCustomer;
using Northwind.WebUI.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.WebUI.IntegrationTests.Controllers.Customers;

//[Collection(WebUICollection.Definition)]
public class Update : IntegrationTestBase
{
    public Update(TestingDatabaseFixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
    public async Task GivenUpdateCustomerCommand_ReturnsSuccessStatusCode()
    {
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

        var content = Utilities.GetRequestContent(command);

        var response = await client.PutAsync($"/api/customers/{command.Id}", content);

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GivenUpdateCustomerCommandWithInvalidId_ReturnsNotFoundStatusCode()
    {
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

        var content = Utilities.GetRequestContent(invalidCommand);

        var response = await client.PutAsync($"/api/customers/{invalidCommand.Id}", content);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}