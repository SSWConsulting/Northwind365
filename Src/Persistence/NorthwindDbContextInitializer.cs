using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using Bogus;
using Bogus.DataSets;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Northwind.Application.Common.Interfaces;
using Northwind.Domain.Categories;
using Northwind.Domain.Customers;
using Northwind.Domain.Employees;
using Northwind.Domain.Orders;
using Northwind.Domain.Products;
using Northwind.Domain.Shipping;
using Northwind.Domain.Supplying;

using Address = Northwind.Domain.Common.Address;

namespace Northwind.Persistence;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class NorthwindDbContextInitializer
{
    private readonly ILogger<NorthwindDbContextInitializer> _logger;
    private readonly NorthwindDbContext _dbContext;
    private readonly IUserManager _userManager;

    private const int NumCategories = 10;
    private const int NumTerritoriesPerRegion = 10;
    private const int NumCustomers = 100;
    private const int NumEmployees = 100;
    private const int NumShippers = 3;
    private const int NumSuppliers = 30;
    private const int NumProducts = 100;
    private const int NumOrders = 1000;

    public NorthwindDbContextInitializer(ILogger<NorthwindDbContextInitializer> logger, NorthwindDbContext dbContext,
        IUserManager userManager)
    {
        _logger = logger;
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public async Task InitializeAsync()
    {
        try
        {
            if (_dbContext.Database.IsSqlServer())
                await _dbContext.Database.MigrateAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while migrating or initializing the database");
            throw;
        }
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await SeedCustomersAsync(cancellationToken);
            await SeedCategoriesAsync(cancellationToken);
            await SeedRegionsAsync(cancellationToken);
            await SeedTerritoriesAsync(cancellationToken);
            await SeedEmployeesAsync(cancellationToken);
            await SeedShippersAsync(cancellationToken);
            await SeedSuppliersAsync(cancellationToken);
            await SeedProductsAsync(cancellationToken);
            await SeedOrdersAsync(cancellationToken);
            await SeedUsersAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while seeding the database");
            throw;
        }
    }

    private async Task SeedCustomersAsync(CancellationToken cancellationToken)
    {
        if (_dbContext.Customers.Any())
            return;

        var faker = new Faker<Customer>().CustomInstantiator(f => Customer.Create(
            f.Company.CompanyName(0),
            f.Name.FullName(),
            f.Name.JobTitle(),
            AddressFaker.Generate(),
            f.Phone.PhoneNumber(),
            f.Phone.PhoneNumber()
        ));

        var customers = faker.Generate(NumCustomers);
        await _dbContext.Customers.AddRangeAsync(customers, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }


    private async Task SeedCategoriesAsync(CancellationToken cancellationToken)
    {
        if (_dbContext.Categories.Any())
            return;

        var faker = new Faker<Category>()
            .CustomInstantiator(f => new Category
            (
                f.Commerce.Categories(1)[0],
                f.Lorem.Sentence(10),
                // TODO: Replace with Image URL
                f.Random.Bytes(5)
            ));

        var categories = faker.Generate(NumCategories);
        await _dbContext.Categories.AddRangeAsync(categories, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task SeedRegionsAsync(CancellationToken cancellationToken)
    {
        if (_dbContext.Region.Any())
            return;

        var regions = new[]
        {
            new Region { RegionId = 1, RegionDescription = "Eastern" },
            new Region { RegionId = 2, RegionDescription = "Western" },
            new Region { RegionId = 3, RegionDescription = "Northern" },
            new Region { RegionId = 4, RegionDescription = "Southern" }
        };

        _dbContext.Region.AddRange(regions);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task SeedTerritoriesAsync(CancellationToken cancellationToken)
    {
        if (_dbContext.Territories.Any())
            return;

        var faker = new Faker();

        foreach (var region in await _dbContext.Region.ToListAsync(cancellationToken: cancellationToken))
        {
            for (int i = 0; i < NumTerritoriesPerRegion; i++)
            {
                _dbContext.Territories.Add(new Territory
                {
                    TerritoryId = faker.Commerce.Ean8(),
                    RegionId = region.RegionId,
                    TerritoryDescription = faker.Lorem.Sentence(3)
                });
            }
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private Faker<Address> AddressFaker = new Faker<Address>()
        .CustomInstantiator(f => new Address(
            f.Address.StreetAddress(),
            f.Address.City(),
            f.Address.Country(),
            f.Address.ZipCode(),
            f.Address.State()));

    private async Task SeedEmployeesAsync(CancellationToken cancellationToken)
    {
        if (_dbContext.Employees.Any())
            return;

        var territories = await _dbContext.Territories.ToListAsync();

        var faker = new Faker<Employee>()
            .CustomInstantiator(f => new Employee
            {
                BirthDate = f.Date.Past(50),
                Address = AddressFaker.Generate(),
                Extension = f.Random.ReplaceNumbers("####"),
                FirstName = f.Name.FirstName(),
                HireDate = f.Date.Past(10),
                HomePhone = f.Phone.PhoneNumber(),
                LastName = f.Name.LastName(),
                Notes = f.Lorem.Sentence(10),
                Title = f.Name.JobTitle(),
                TitleOfCourtesy = f.Name.Prefix(),
                Photo = f.Random.Bytes(5),
                PhotoPath = f.Image.PicsumUrl(),
                EmployeeTerritories = f.PickRandom(territories, f.Random.Int(2, 10))
                    .Select(t => new EmployeeTerritory { TerritoryId = t.TerritoryId }).ToList()
            });

        var employees = faker.Generate(NumEmployees);
        await _dbContext.Employees.AddRangeAsync(employees, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task SeedShippersAsync(CancellationToken cancellationToken)
    {
        if (_dbContext.Shippers.Any())
            return;

        var faker = new Faker<Shipper>().CustomInstantiator(f => new Shipper
        {
            CompanyName = f.Company.CompanyName(), Phone = f.Phone.PhoneNumber()
        });

        var shippers = faker.Generate(NumShippers);
        await _dbContext.Shippers.AddRangeAsync(shippers, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task SeedSuppliersAsync(CancellationToken cancellationToken)
    {
        if (_dbContext.Suppliers.Any())
            return;

        var faker = new Faker<Supplier>().CustomInstantiator(f => Supplier.Create
        (
            f.Company.CompanyName(),
            f.Name.FullName(),
            f.Name.JobTitle(),
            AddressFaker.Generate(),
            f.Phone.PhoneNumber(),
            f.Phone.PhoneNumber(),
            f.Internet.Url()
        ));

        var suppliers = faker.Generate(NumSuppliers);
        await _dbContext.Suppliers.AddRangeAsync(suppliers, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task SeedProductsAsync(CancellationToken cancellationToken)
    {
        if (_dbContext.Products.Any())
            return;

        var suppliers = await _dbContext.Suppliers.ToListAsync(cancellationToken: cancellationToken);
        var categories = await _dbContext.Categories.ToListAsync(cancellationToken: cancellationToken);

        var faker = new Faker<Product>().CustomInstantiator(f => new Product
        {
            ProductName = f.Commerce.ProductName(),
            Supplier = f.PickRandom(suppliers),
            Category = f.PickRandom(categories),
            QuantityPerUnit = f.Lorem.Sentence(2),
            UnitPrice = f.Random.Decimal(1, 100),
            UnitsInStock = f.Random.Short(1, 100),
            UnitsOnOrder = f.Random.Short(1, 100),
            ReorderLevel = f.Random.Short(1, 100),
            Discontinued = f.Random.Bool()
        });

        var products = faker.Generate(NumProducts);
        await _dbContext.Products.AddRangeAsync(products, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task SeedOrdersAsync(CancellationToken cancellationToken)
    {
        if (_dbContext.Orders.Any())
            return;

        var customer = await _dbContext.Customers.ToListAsync(cancellationToken: cancellationToken);
        var employees = await _dbContext.Employees.ToListAsync(cancellationToken: cancellationToken);
        var shippers = await _dbContext.Shippers.ToListAsync(cancellationToken: cancellationToken);
        var products = await _dbContext.Products.ToListAsync(cancellationToken: cancellationToken);

        var orderDetailFaker = new Faker<OrderDetail>().CustomInstantiator(f => new OrderDetail
        {
            Product = f.PickRandom(products),
            UnitPrice = f.Random.Decimal(1, 50),
            Quantity = f.Random.Short(1, 5),
            Discount = f.Random.Float(0, 20)
        });

        var faker = new Faker<Order>().CustomInstantiator(f => new Order
        {
            CustomerId = f.PickRandom(customer).Id,
            Employee = f.PickRandom(employees),
            OrderDate = f.Date.Past(10),
            RequiredDate = f.Date.Future(10),
            ShippedDate = f.Date.Future(10),
            Shipper = f.PickRandom(shippers),
            Freight = f.Random.Decimal(1, 100),
            ShipName = f.Company.CompanyName(),
            ShipAddress = AddressFaker.Generate(),
        });

        var orders = faker.Generate(NumOrders);
        var randomFaker = new Faker();

        foreach (var order in orders)
        {
            var orderDetails = orderDetailFaker.Generate(randomFaker.Random.Int(1, 10));
            order.AddOrderDetails(orderDetails);
        }

        await _dbContext.Orders.AddRangeAsync(orders, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task SeedUsersAsync(CancellationToken cancellationToken)
    {
        var employees = await _dbContext.Employees
            .Include(e => e.DirectReports)
            .Where(e => e.UserId == null)
            .ToListAsync(cancellationToken);

        if (employees.Any())
        {
            foreach (var employee in employees)
            {
                var userName = $"{employee.FirstName}@northwind".ToLower();
                var userId = await _userManager.CreateUserAsync(userName, "Northwind1!");
                employee.UserId = userId;

                if (employee.DirectReports.Any())
                {
                    // TODO: Add to manager role
                }
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}