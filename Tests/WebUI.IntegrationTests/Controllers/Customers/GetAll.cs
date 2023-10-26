using Common.Factories;
using Common.Fixtures;
using FluentAssertions;
using Northwind.Application.Customers.Queries.GetCustomersList;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.WebUI.IntegrationTests.Controllers.Customers;

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
        var vm = await client.GetFromJsonAsync<CustomersListVm>("/api/customers");

        // Assert
        vm.Should().NotBeNull();
        vm.Should().BeOfType<CustomersListVm>();
        vm.Customers.Should().NotBeEmpty();
        vm.Customers.Should().HaveCount(1);
    }
}