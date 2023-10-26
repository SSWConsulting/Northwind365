using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Northwind.Infrastructure.Persistence;

namespace Northwind.Infrastructure.Identity;

// public class ApplicationDbContextFactory : DesignTimeDbContextFactoryBase<ApplicationDbContext>
// {
//     protected override ApplicationDbContext CreateNewInstance(DbContextOptions<ApplicationDbContext> options)
//     {
//         return new ApplicationDbContext(options);
//     }
// }

public sealed class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer();
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}