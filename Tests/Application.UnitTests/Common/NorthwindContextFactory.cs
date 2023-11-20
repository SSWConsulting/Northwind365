using Common.Factories;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Common.Interfaces;
using Northwind.Infrastructure.Persistence;
using NSubstitute;

namespace Northwind.Application.UnitTests.Common;

public static class NorthwindContextFactory
{
    public static NorthwindDbContext Create()
    {
        var options = new DbContextOptionsBuilder<NorthwindDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new NorthwindDbContext(options, Substitute.For<ICurrentUserService>(), Substitute.For<IDateTime>());
        context.Database.EnsureCreated();

        return context;
    }

    public static void Destroy(NorthwindDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}