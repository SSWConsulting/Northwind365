using Microsoft.Extensions.DependencyInjection;
using Northwind.Infrastructure.Identity;
using Northwind.Infrastructure.IntegrationTests.TestHelpers;
using Northwind.Infrastructure.Persistence;
using Northwind.Persistence;
using static Northwind.Infrastructure.Persistence.NorthwindDbContextInitializer;

namespace Northwind.Infrastructure.IntegrationTests;

public class NorthwindDbContextInitializerTests : IntegrationTestBase
{
    public NorthwindDbContextInitializerTests(TestingDatabaseFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task SeedAsync_AfterInitializeAsync_PopulatesDatabase()
    {
        // Arrange
        using var scope = Fixture.ScopeFactory.CreateScope();

        var identitiesDbInitializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
        await identitiesDbInitializer.InitializeAsync();

        var northwindDbInitializer = scope.ServiceProvider.GetRequiredService<NorthwindDbContextInitializer>();
        await northwindDbInitializer.InitializeAsync();

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