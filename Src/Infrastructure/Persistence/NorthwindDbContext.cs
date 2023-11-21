using Microsoft.EntityFrameworkCore;
using Northwind.Application.Common.Interfaces;
using Northwind.Domain.Categories;
using Northwind.Domain.Common.Base;
using Northwind.Domain.Customers;
using Northwind.Domain.Employees;
using Northwind.Domain.Orders;
using Northwind.Domain.Products;
using Northwind.Domain.Shipping;
using Northwind.Domain.Supplying;
using Northwind.Infrastructure.Persistence.Interceptors;

namespace Northwind.Infrastructure.Persistence;

public class NorthwindDbContext(DbContextOptions<NorthwindDbContext> options,
        EntitySaveChangesInterceptor saveChangesInterceptor,
        DispatchDomainEventsInterceptor dispatchDomainEventsInterceptor)
    : DbContext(options), INorthwindDbContext
{
    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Customer> Customers => Set<Customer>();

    public DbSet<Employee> Employees => Set<Employee>();

    public DbSet<EmployeeTerritory> EmployeeTerritories => Set<EmployeeTerritory>();

    public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<Product> Products => Set<Product>();

    public DbSet<Region> Region => Set<Region>();

    public DbSet<Shipper> Shippers => Set<Shipper>();

    public DbSet<Supplier> Suppliers => Set<Supplier>();

    public DbSet<Territory> Territories => Set<Territory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NorthwindDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Order of the interceptors is important
        optionsBuilder.AddInterceptors(saveChangesInterceptor, dispatchDomainEventsInterceptor);
    }
}