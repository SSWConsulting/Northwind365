using Microsoft.EntityFrameworkCore;

using System.Threading;
using System.Threading.Tasks;

using Northwind.Domain.Categories;
using Northwind.Domain.Customers;
using Northwind.Domain.Employees;
using Northwind.Domain.Orders;
using Northwind.Domain.Products;
using Northwind.Domain.Shipping;
using Northwind.Domain.Supplying;

namespace Northwind.Application.Common.Interfaces;

public interface INorthwindDbContext
{
    DbSet<Category> Categories { get; }

    DbSet<Customer> Customers { get; }

    DbSet<Employee> Employees { get; }

    DbSet<EmployeeTerritory> EmployeeTerritories { get; }

    DbSet<OrderDetail> OrderDetails { get; }

    DbSet<Order> Orders { get; }

    DbSet<Product> Products { get; }

    DbSet<Region> Region { get; }

    DbSet<Shipper> Shippers { get; }

    DbSet<Supplier> Suppliers { get; }

    DbSet<Territory> Territories { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}