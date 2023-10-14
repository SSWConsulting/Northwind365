using System.Collections.Generic;
using System.Security.Claims;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Northwind.Application.Common.Interfaces;
using Northwind.Infrastructure.Files;
using Northwind.Infrastructure.Identity;
using Northwind.Infrastructure.Persistence;
using Northwind.Infrastructure.Services;
using Northwind.Persistence;

namespace Northwind.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        AddFiles(services);
        AddIdentity(services, configuration, environment);
        AddPersistence(services, configuration);
        AddServices(services);

        return services;
    }

    private static void AddFiles(IServiceCollection services)
    {
        services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();
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

        services.AddScoped<INorthwindDbContext>(provider => provider.GetService<NorthwindDbContext>());
        services.AddScoped<NorthwindDbContextInitializer>();
    }

    private static void AddIdentity(IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        services.AddScoped<IUserManager, UserManagerService>();

        services.AddScoped<ApplicationDbContextInitializer>();

        var connectionString = configuration.GetConnectionString("NorthwindDatabase");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddDefaultIdentity<ApplicationUser>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        // TODO DM: Is this needed?
        services.AddControllersWithViews()
            .ConfigureApplicationPartManager(apm =>
            {
                apm.ApplicationParts
                    .Remove(apm.ApplicationParts.Single(ap =>
                        ap.Name == "Microsoft.AspNetCore.ApiAuthorization.IdentityServer"));
            });

        var identityServerBuilder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://docs.duendesoftware.com/identityserver/v5/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;
            })
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b =>
                    b.UseSqlServer(connectionString,
                        dbOpts => dbOpts.MigrationsAssembly(typeof(IInfrastructureMarker).Assembly.FullName));
            })
            // this is something you will want in production to reduce load on and requests to the DB
            //.AddConfigurationStoreCache()
            //
            // this adds the operational data from DB (codes, tokens, consents)
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b =>
                    b.UseSqlServer(connectionString,
                        dbOpts => dbOpts.MigrationsAssembly(typeof(IInfrastructureMarker).Assembly.FullName));
            })
            .AddServerSideSessions();

        if (environment.IsEnvironment("Test") || environment.IsDevelopment())
        {
            identityServerBuilder
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
                {
                    options.Clients.Add(new Client
                    {
                        ClientId = "Northwind.IntegrationTests",
                        AllowedGrantTypes = { GrantType.ResourceOwnerPassword },
                        ClientSecrets = { new Secret("secret".Sha256()) },
                        AllowedScopes = { "Northwind.WebUI", "openid", "profile" }
                    });
                })
                .AddTestUsers(new List<TestUser>
                {
                    new TestUser
                    {
                        SubjectId = "f26da293-02fb-4c90-be75-e4aa51e0bb17",
                        Username = "jason@northwind",
                        Password = "Northwind1!",
                        Claims = new List<Claim> { new Claim(JwtClaimTypes.Email, "jason@northwind") }
                    }
                });
        }
        else
        {
            identityServerBuilder
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();
        }

        services.AddAuthentication()
            .AddIdentityServerJwt();
    }
}