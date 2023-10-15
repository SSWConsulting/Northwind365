using Northwind.Application.Customers.Commands.CreateCustomer;
using Northwind.WebUI.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.WebUI.IntegrationTests.Controllers.Customers;

public class Create : IntegrationTestBase
{
    public Create(TestingDatabaseFixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
    public async Task GivenCreateCustomerCommand_ReturnsSuccessStatusCode()
    {
        var client = await GetAuthenticatedClientAsync();

        var command = new CreateCustomerCommand
        (
            "456",
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

        var response = await client.PostAsync($"/api/customers", content);

        response.EnsureSuccessStatusCode();
    }
}