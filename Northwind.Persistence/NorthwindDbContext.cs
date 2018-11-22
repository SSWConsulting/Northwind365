using Microsoft.EntityFrameworkCore;
using Northwind.Domain.Entities;
using Northwind.Persistence.Extensions;
using Northwind.Persistence.QueryTypes;

namespace Northwind.Persistence
{
    public class NorthwindDbContext : DbContext
    {
        public NorthwindDbContext(DbContextOptions<NorthwindDbContext> options)
            : base(options)
        {
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

        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyAllConfigurations();

          
            modelBuilder.Query<CustomersMostPurchasedProducts>()
                .ToQuery(() => this.Query<CustomersMostPurchasedProducts>().FromSql(@"
            select 
            	c.CustomerID
            	, c.CompanyName
            	, p.ProductID
            	, p.ProductName
            	, qtyCounts.QuantityPurchased
            	from Customers c 
            	inner join
            		(select  
            			o.CustomerID
            			, od.ProductID 
            			,sum(od.Quantity) as QuantityPurchased
            		from [Order Details] od
            		inner join [Orders] o on od.OrderID = o.OrderID
            		group by o.CustomerID, od.ProductID)  qtyCounts on c.CustomerID = qtyCounts.CustomerID
            	inner join Products p on p.ProductID = qtyCounts.ProductID
            "));
        }
    }
}
