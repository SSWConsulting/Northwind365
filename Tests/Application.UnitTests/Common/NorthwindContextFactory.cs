using System;

using Microsoft.EntityFrameworkCore;

using Northwind.Persistence;

namespace Northwind.Application.UnitTests.Common;

public class NorthwindContextFactory
{
    public static NorthwindDbContext Create()
    {
        var options = new DbContextOptionsBuilder<NorthwindDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new NorthwindDbContext(options);

        context.Database.EnsureCreated();

        // context.Customers.AddRange(new[] {
        //     new Customer { Id = "ADAM", ContactName = "Adam Cogan" },
        //     new Customer { Id = "JASON", ContactName = "Jason Taylor" },
        //     new Customer { Id = "BREND", ContactName = "Brendan Richards" },
        // });

        var customers = CustomerFactory.Generate(3);
        context.Customers.AddRange(customers);
        context.SaveChanges();

        return context;
    }

    public static void Destroy(NorthwindDbContext context)
    {
        context.Database.EnsureDeleted();

        context.Dispose();
    }
}