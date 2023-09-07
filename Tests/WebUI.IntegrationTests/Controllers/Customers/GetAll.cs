using System.Threading.Tasks;
using Northwind.Application.Customers.Queries.GetCustomersList;
using Northwind.WebUI.IntegrationTests.Common;
using Xunit;

namespace Northwind.WebUI.IntegrationTests.Controllers.Customers;

public class GetAll : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public GetAll(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ReturnsCustomersListViewModel()
    {
        var client = await _factory.GetAuthenticatedClientAsync();

        var response = await client.GetAsync("/api/customers/getall");

        response.EnsureSuccessStatusCode();

        var vm = await Utilities.GetResponseContent<CustomersListVm>(response);

        Assert.IsType<CustomersListVm>(vm);
        Assert.NotEmpty(vm.Customers);
    }
}
