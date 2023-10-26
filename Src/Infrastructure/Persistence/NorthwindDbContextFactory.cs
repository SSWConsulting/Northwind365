using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Northwind.Application.Common.Interfaces;
using Northwind.Infrastructure.Services;

namespace Northwind.Infrastructure.Persistence;

// public class NorthwindDbContextFactory : DesignTimeDbContextFactoryBase<NorthwindDbContext>
// {
//     protected override NorthwindDbContext CreateNewInstance(DbContextOptions<NorthwindDbContext> options)
//     {
//         return new NorthwindDbContext(options, new DesignTimeUserService(), new MachineDateTime());
//     }
// }

public sealed class NorthwindDbContextFactory : IDesignTimeDbContextFactory<NorthwindDbContext>
{
    public NorthwindDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<NorthwindDbContext>();
        optionsBuilder.UseSqlServer();
        return new NorthwindDbContext(optionsBuilder.Options, new DesignTimeUserService(), new MachineDateTime());
    }
}

internal class DesignTimeUserService : ICurrentUserService
{
    public string GetUserId()
    {
        return "00000000-0000-0000-0000-000000000000";
    }
}