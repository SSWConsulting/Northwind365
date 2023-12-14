using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Application.Common.Interfaces;
using Northwind.Application.Products.Queries.GetProductsFile;
using Northwind.Infrastructure.Files;
using Northwind.Infrastructure.Identity;
using Northwind.Infrastructure.Persistence;
using Northwind.Infrastructure.Persistence.Interceptors;
using Northwind.Infrastructure.Services;

namespace Northwind.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddFiles(services);
        AddIdentity(services, configuration);
        AddPersistence(services, configuration);
        AddServices(services);

        return services;
    }

    private static void AddFiles(IServiceCollection services)
    {
        services.AddTransient<ICsvBuilder, CsvBuilder>();
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddTransient<INotificationService, NotificationService>();
        services.AddTransient<IDateTime, MachineDateTime>();
    }

    private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<NorthwindDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("NorthwindDatabase")));

        services.AddScoped<INorthwindDbContext>(provider => provider.GetRequiredService<NorthwindDbContext>());
        services.AddScoped<NorthwindDbContextInitializer>();

        services.AddScoped<EntitySaveChangesInterceptor>();
        services.AddScoped<DispatchDomainEventsInterceptor>();
    }

    private static void AddIdentity(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("NorthwindDatabase");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IUserManager, UserManagerService>();

        services.AddScoped<ApplicationDbContextInitializer>();

        services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);

        services.AddAuthorizationBuilder();

        services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();
    }
}