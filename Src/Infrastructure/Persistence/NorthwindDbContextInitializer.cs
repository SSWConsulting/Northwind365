using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Northwind.Application.Common.Interfaces;
using Northwind.Domain.Categories;
using Northwind.Domain.Common;
using Northwind.Domain.Customers;
using Northwind.Domain.Employees;
using Northwind.Domain.Orders;
using Northwind.Domain.Products;
using Northwind.Domain.Shipping;
using Northwind.Domain.Supplying;
using System.Diagnostics.CodeAnalysis;

namespace Northwind.Infrastructure.Persistence;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class NorthwindDbContextInitializer
{
    private readonly ILogger<NorthwindDbContextInitializer> _logger;
    private readonly NorthwindDbContext _dbContext;
    private readonly IUserManager _userManager;

    public const int NumCategories = 10;
    public const int NumTerritoriesPerRegion = 10;
    public const int NumCustomers = 100;
    public const int NumEmployees = 100;
    public const int NumShippers = 3;
    public const int NumSuppliers = 30;
    public const int NumProducts = 100;
    public const int NumOrders = 1000;
    public const int MinEmployeeTerritories = 2;
    public const int MaxEmployeeTerritories = 10;
    public const int MinOrderDetails = 1;
    public const int MaxOrderDetails = 10;
    public const int NumRegions = 4;

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

    // NOTE: Consider splitting data between static and sample data
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
            new CustomerId(f.Commerce.Ean8()),
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
            Region.Create("Eastern"), Region.Create("Western"), Region.Create("Northern"),
            Region.Create("Southern"),
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
            for (var i = 0; i < NumTerritoriesPerRegion; i++)
            {
                var territoryId = new TerritoryId(faker.Commerce.Ean8());
                var regionId = region.Id;
                var territoryDescription = faker.Lorem.Sentence(3);
                var territory = Territory.Create(territoryId, regionId, territoryDescription);

                _dbContext.Territories.Add(territory);
            }
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private Faker<Address> AddressFaker = new Faker<Address>()
        .CustomInstantiator(f => new Address(
            f.Address.StreetAddress(),
            f.Address.City(),
            f.Address.State(),
            f.Address.ZipCode(),
            f.Address.Country()
            ));

    private async Task SeedEmployeesAsync(CancellationToken cancellationToken)
    {
        if (_dbContext.Employees.Any())
            return;

        var territories = await _dbContext.Territories.ToListAsync(cancellationToken);

        var faker = new Faker<Employee>()
            .CustomInstantiator(f => Employee.Create
            (
                f.Date.Past(50),
                AddressFaker.Generate(),
                f.Random.ReplaceNumbers("####"),
                f.Name.FirstName(),
                f.Phone.PhoneNumber(),
                f.Date.Past(10),
                f.Name.LastName(),
                f.Lorem.Sentence(10),
                f.Image.PicsumUrl(),
                f.Random.Bytes(5),
                f.Name.JobTitle(),
                f.Name.Prefix(),
                f.PickRandom(territories, f.Random.Int(MinEmployeeTerritories, MaxEmployeeTerritories))
            ));

        var employees = faker.Generate(NumEmployees);
        await _dbContext.Employees.AddRangeAsync(employees, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task SeedShippersAsync(CancellationToken cancellationToken)
    {
        if (_dbContext.Shippers.Any())
            return;

        var faker = new Faker<Shipper>().CustomInstantiator(f => Shipper.Create
        (
            f.Company.CompanyName(),
            f.Phone.PhoneNumber()
        ));

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

        var faker = new Faker<Product>().CustomInstantiator(f =>
        {
            var product = Product.Create
            (
                f.Commerce.ProductName(),
                f.PickRandom(categories).Id,
                f.PickRandom(suppliers).Id,
                f.Random.Decimal(1, 100),
                f.Random.Bool()
            );

            product.UpdateQuantityPerUnit(f.Lorem.Sentence(2));
            product.UpdateUnitsInStock(f.Random.Short(1, 100));
            product.UpdateUnitsOnOrder(f.Random.Short(1, 100));
            product.UpdateReorderLevel(f.Random.Short(1, 100));

            return product;
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

        var faker = new Faker<Order>().CustomInstantiator(f => Order.Create
        (
            f.PickRandom(customer).Id,
            f.PickRandom(employees).Id,
            f.Date.Past(10),
            f.Date.Future(10),
            f.Date.Future(10),
            f.PickRandom(shippers).Id,
            f.Random.Decimal(1, 100),
            f.Company.CompanyName(),
            AddressFaker.Generate()
        ));

        var orders = faker.Generate(NumOrders);
        var randomFaker = new Faker();

        foreach (var order in orders)
        {
            var numOrderDetails = randomFaker.Random.Int(MinOrderDetails, MaxOrderDetails);
            for (var i = 0; i < numOrderDetails; i++)
            {
                var productId = randomFaker.PickRandom(products).Id;
                var unitPrice = randomFaker.Random.Decimal(1, 50);
                var quantity = randomFaker.Random.Short(1, 5);
                var discount = randomFaker.Random.Float(0, 20);
                order.AddOrderDetails(productId, unitPrice, quantity, discount);
            }
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
                var userName = $"{employee.FirstName}.{employee.LastName}@northwind".ToLower();
                var userId = await _userManager.CreateUserAsync(userName, "Northwind1!");

                // NOTE: If the user already exists, then the user id will be null
                if (!string.IsNullOrWhiteSpace(userId))
                {
                    employee.UpdateUserId(userId);
                }

                if (employee.DirectReports.Any())
                {
                    // TODO: Add to manager role
                }
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}