using FluentAssertions;
using Northwind.Application.Customers.Queries.GetCustomersList;
using Northwind.WebUI.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.WebUI.IntegrationTests.Controllers.Customers;

//[Collection(WebUICollection.Definition)]
public class GetAll : IntegrationTestBase
{
    public GetAll(TestingDatabaseFixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
    public async Task ReturnsCustomersListViewModel()
    {
        var client = await GetAuthenticatedClientAsync();

        var response = await client.GetAsync("/api/customers");

        response.EnsureSuccessStatusCode();

        var vm = await Utilities.GetResponseContent<CustomersListVm>(response);

        vm.Should().BeOfType<CustomersListVm>();
        vm.Customers.Should().NotBeEmpty();
    }
}