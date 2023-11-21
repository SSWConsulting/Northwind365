using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Common.Interfaces;
using Northwind.Infrastructure.Persistence;
using Northwind.Infrastructure.Persistence.Interceptors;
using NSubstitute;

namespace Northwind.Application.UnitTests.Common;

public static class NorthwindContextFactory
{
    public static NorthwindDbContext Create()
    {
        var options = new DbContextOptionsBuilder<NorthwindDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new NorthwindDbContext(options, new EntitySaveChangesInterceptor(
            Substitute.For<ICurrentUserService>(),
            Substitute.For<IDateTime>()), new DispatchDomainEventsInterceptor(Substitute.For<IMediator>()));
        context.Database.EnsureCreated();

        return context;
    }

    public static void Destroy(NorthwindDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}