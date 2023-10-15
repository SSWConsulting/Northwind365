using Common.Factories;
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
        // Arrange
        var client = await GetAuthenticatedClientAsync();
        var customer = CustomerFactory.Generate();
        await AddEntityAsync(customer);

        // Act
        var response = await client.GetAsync("/api/customers");

        // Assert
        response.EnsureSuccessStatusCode();

        var vm = await Utilities.GetResponseContent<CustomersListVm>(response);

        vm.Should().BeOfType<CustomersListVm>();
        vm.Customers.Should().NotBeEmpty();
        vm.Customers.Should().HaveCount(1);
    }
}