using Northwind.Application.Customers.Queries.GetCustomersList;
using Northwind.WebUI.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.WebUI.IntegrationTests.Controllers.Customers;

[Collection(WebUICollection.Definition)]
public class GetAll
{
    private readonly CustomWebApplicationFactory _factory;

    public GetAll(CustomWebApplicationFactory factory, ITestOutputHelper output)
    {
        _factory = factory;
        _factory.Output = output;
    }

    [Fact]
    public async Task ReturnsCustomersListViewModel()
    {
        var client = await _factory.GetAuthenticatedClientAsync();

        var response = await client.GetAsync("/api/customers");

        response.EnsureSuccessStatusCode();

        var vm = await Utilities.GetResponseContent<CustomersListVm>(response);

        Assert.IsType<CustomersListVm>(vm);
        Assert.NotEmpty(vm.Customers);
    }
}