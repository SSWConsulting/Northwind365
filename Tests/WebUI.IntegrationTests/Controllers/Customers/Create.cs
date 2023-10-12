using System.Net.Http;
using System.Threading.Tasks;
using Northwind.Application.Customers.Commands.CreateCustomer;
using Northwind.WebUI.IntegrationTests.Common;
using Xunit;

namespace Northwind.WebUI.IntegrationTests.Controllers.Customers;

public class Create : IClassFixture<CustomWebApplicationFactory<IWebUiMarker>>
{
    private readonly CustomWebApplicationFactory<IWebUiMarker> _factory;

    public Create(CustomWebApplicationFactory<IWebUiMarker> factory)
    {
        _factory = factory;
    }

    // TODO: Add back in
    // [Fact]
    // public async Task GivenCreateCustomerCommand_ReturnsSuccessStatusCode()
    // {
    //     var client = await _factory.GetAuthenticatedClientAsync();
    //
    //     var command = new CreateCustomerCommand
    //     {
    //         // TODO: Look up ID from DB
    //         Id = "123",
    //         Address = "Obere Str. 57",
    //         City = "Berlin",
    //         CompanyName = "Alfreds Futterkiste",
    //         ContactName = "Maria Anders",
    //         ContactTitle = "Sales Representative",
    //         Country = "Germany",
    //         Fax = "030-0076545",
    //         Phone = "030-0074321",
    //         PostalCode = "12209"
    //     };
    //
    //     var content = Utilities.GetRequestContent(command);
    //
    //     var response = await client.PostAsync($"/api/customers/create", content);
    //
    //     response.EnsureSuccessStatusCode();
    // }
}