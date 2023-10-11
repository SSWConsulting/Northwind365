using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Northwind.Application.Common.Interfaces;
using Northwind.Infrastructure.Identity;
using Northwind.Persistence;

namespace Northwind.Infrastructure.IntegrationTests.TestHelpers;

internal class IntegrationTestWebApplicationFactory : WebApplicationFactory<Program>
{
    public DatabaseContainer Database { get; }

    // ReSharper disable once ConvertConstructorToMemberInitializers
    public IntegrationTestWebApplicationFactory()
    {
        Database = new DatabaseContainer();
    }

    protected override void ConfigureWebHost(IWebHostBuilder webHostBuilder)
    {
        webHostBuilder.ConfigureTestServices(services => services
            .ReplaceDbContext<ApplicationDbContext>(Database)
            .ReplaceDbContext<NorthwindDbContext>(Database));
    }
}

internal static class DbContextExt
{
    internal static IServiceCollection ReplaceDbContext<T>(this IServiceCollection services,
        DatabaseContainer databaseContainer) where T : DbContext
    {
        services
            .RemoveAll<DbContextOptions<T>>()
            .RemoveAll<T>()
            .AddDbContext<T>((_, options) =>
                options.UseSqlServer(databaseContainer.ConnectionString,
                    b => b.MigrationsAssembly(typeof(T).Assembly.FullName)));

        return services;
    }
}