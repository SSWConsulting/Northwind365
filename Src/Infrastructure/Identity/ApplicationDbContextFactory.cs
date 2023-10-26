using Microsoft.EntityFrameworkCore;
using Northwind.Infrastructure.Persistence;

namespace Northwind.Infrastructure.Identity;

public class ApplicationDbContextFactory : DesignTimeDbContextFactoryBase<ApplicationDbContext>
{
    protected override ApplicationDbContext CreateNewInstance(DbContextOptions<ApplicationDbContext> options)
    {
        return new ApplicationDbContext(options);
    }
}