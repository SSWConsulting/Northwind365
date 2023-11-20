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

namespace Northwind.Infrastructure.Persistence;

public class NorthwindDbContext(DbContextOptions<NorthwindDbContext> options,
        ICurrentUserService currentUserService,
        IDateTime dateTime)
    : DbContext(options), INorthwindDbContext
{
    public DbSet<Category> Categories { get; set; } = null!;

    public DbSet<Customer> Customers { get; set; } = null!;

    public DbSet<Employee> Employees { get; set; } = null!;

    public DbSet<EmployeeTerritory> EmployeeTerritories { get; set; } = null!;

    public DbSet<OrderDetail> OrderDetails { get; set; } = null!;

    public DbSet<Order> Orders { get; set; } = null!;

    public DbSet<Product> Products { get; set; } = null!;

    public DbSet<Region> Region { get; set; } = null!;

    public DbSet<Shipper> Shippers { get; set; } = null!;

    public DbSet<Supplier> Suppliers { get; set; } = null!;

    public DbSet<Territory> Territories { get; set; } = null!;

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        ChangeTracker.DetectChanges();

        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.SetCreated(dateTime.Now, currentUserService.GetUserId());
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.SetUpdated(dateTime.Now, currentUserService.GetUserId());
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NorthwindDbContext).Assembly);
    }
}