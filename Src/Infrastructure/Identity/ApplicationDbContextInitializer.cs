using System.Diagnostics.CodeAnalysis;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Northwind.Infrastructure.Identity;

public class ApplicationDbContextInitializer(ILogger<ApplicationDbContextInitializer> logger,
    ApplicationDbContext dbContext)
{
    public Task<bool> CanConnect()
    {
        return dbContext.Database.CanConnectAsync();
    }

    // ReSharper disable once UnusedMember.Global
    public async Task EnsureDeleted()
    {
        try
        {
            if (dbContext.Database.IsSqlServer())
                await dbContext.Database.EnsureDeletedAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while migrating or initializing the database");
            throw;
        }
    }

    public async Task InitializeAsync()
    {
        try
        {
            if (dbContext.Database.IsSqlServer())
                await dbContext.Database.MigrateAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while migrating or initializing the database");
            throw;
        }
    }
}