using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Northwind.Application.Customers.Commands.UpdateCustomer;
using Northwind.WebUI.IntegrationTests.Common;
using Xunit;

namespace Northwind.WebUI.IntegrationTests.Controllers.Customers;

public class Update : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public Update(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GivenUpdateCustomerCommand_ReturnsSuccessStatusCode()
    {
        var client = await _factory.GetAuthenticatedClientAsync();

        var command = new UpdateCustomerCommand
        {
            // TODO: Look up ID from DB
            Id = "123",
            Address = "Obere Str. 57",
            City = "Berlin",
            CompanyName = "Alfreds Futterkiste",
            ContactName = "Maria Anders",
            ContactTitle = "Sales Representative",
            Country = "Germany",
            Fax = "030-0076545",
            Phone = "030-0074321",
            PostalCode = "12209"
        };

        var content = Utilities.GetRequestContent(command);

        var response = await client.PutAsync($"/api/customers/update/{command.Id}", content);

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GivenUpdateCustomerCommandWithInvalidId_ReturnsNotFoundStatusCode()
    {
        var client = await _factory.GetAuthenticatedClientAsync();

        var invalidCommand = new UpdateCustomerCommand
        {
            // TODO: Look up ID from DB
            Id = "123",
            Address = "Obere Str. 57",
            City = "Berlin",
            CompanyName = "Alfreds Futterkiste",
            ContactName = "Maria Anders",
            ContactTitle = "Sales Representative",
            Country = "Germany",
            Fax = "030-0076545",
            Phone = "030-0074321",
            PostalCode = "12209"
        };

        var content = Utilities.GetRequestContent(invalidCommand);

        var response = await client.PutAsync($"/api/customers/update/{invalidCommand.Id}", content);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}