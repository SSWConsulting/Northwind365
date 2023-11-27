using Common.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Infrastructure.IntegrationTests.Common;
using Northwind.Infrastructure.Persistence;
using static Northwind.Infrastructure.Persistence.NorthwindDbContextInitializer;

namespace Northwind.Infrastructure.IntegrationTests;

public class NorthwindDbContextInitializerTests(TestingDatabaseFixture fixture) : IntegrationTestBase(fixture)
{
    [Fact]
    public async Task SeedAsync_AfterInitializeAsync_PopulatesDatabase()
    {
        // Arrange
        using var scope = Fixture.ScopeFactory.CreateScope();

        var northwindDbInitializer = scope.ServiceProvider.GetRequiredService<NorthwindDbContextInitializer>();

        // Act
        await northwindDbInitializer.SeedAsync();

        // Assert
        Context.Categories.Count().Should().Be(NumCategories);
        Context.Customers.Count().Should().Be(NumCustomers);
        Context.Employees.Count().Should().Be(NumEmployees);
        Context.Orders.Count().Should().Be(NumOrders);
        Context.Products.Count().Should().Be(NumProducts);
        Context.Shippers.Count().Should().Be(NumShippers);
        Context.Suppliers.Count().Should().Be(NumSuppliers);
        Context.Region.Count().Should().Be(NumRegions);
        Context.Territories.Count().Should().Be(NumTerritoriesPerRegion * NumRegions);
        Context.EmployeeTerritories.Count().Should().BeGreaterOrEqualTo(MinEmployeeTerritories * NumEmployees);
        Context.OrderDetails.Count().Should().BeGreaterOrEqualTo(NumEmployees * MinOrderDetails);
    }
}