﻿using Microsoft.EntityFrameworkCore;

using Northwind.Application.Common.Interfaces;
using Northwind.Domain.Common;
using Northwind.Domain.Entities;

namespace Northwind.Persistence;

public class NorthwindDbContext : DbContext, INorthwindDbContext
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTime _dateTime;

    public NorthwindDbContext(DbContextOptions<NorthwindDbContext> options)
        : base(options)
    {
    }

    public NorthwindDbContext(
        DbContextOptions<NorthwindDbContext> options, 
        ICurrentUserService currentUserService,
        IDateTime dateTime)
        : base(options)
    {
        _currentUserService = currentUserService;
        _dateTime = dateTime;
    }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<EmployeeTerritory> EmployeeTerritories { get; set; }

    public DbSet<OrderDetail> OrderDetails { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Region> Region { get; set; }

    public DbSet<Shipper> Shippers { get; set; }

    public DbSet<Supplier> Suppliers { get; set; }

    public DbSet<Territory> Territories { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        ChangeTracker.DetectChanges();

        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = _currentUserService.GetUserId();
                entry.Entity.Created = _dateTime.Now;
                
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.LastModifiedBy = _currentUserService.GetUserId();
                entry.Entity.LastModified = _dateTime.Now;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NorthwindDbContext).Assembly);
    }
}
