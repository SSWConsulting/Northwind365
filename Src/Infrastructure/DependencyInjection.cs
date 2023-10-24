using Duende.IdentityServer.AspNetIdentity;
using System.Collections.Generic;
using System.Security.Claims;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Northwind.Application.Common.Interfaces;
using Northwind.Infrastructure.Files;
using Northwind.Infrastructure.Identity;
using Northwind.Infrastructure.Persistence;
using Northwind.Infrastructure.Services;
using Northwind.Persistence;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Northwind.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        AddFiles(services);
        AddIdentityV2(services, configuration, environment);
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

        services.AddScoped<INorthwindDbContext>(provider => provider.GetRequiredService<NorthwindDbContext>());
        services.AddScoped<NorthwindDbContextInitializer>();
    }

    private static void AddIdentityV2(IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        var connectionString = configuration.GetConnectionString("NorthwindDatabase");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IUserManager, UserManagerService>();

        services.AddScoped<ApplicationDbContextInitializer>();



        services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
        services.AddAuthorizationBuilder();

        services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();
    }

    // private static void AddIdentityV1(IServiceCollection services, IConfiguration configuration,
    //     IWebHostEnvironment environment)
    // {
    //     services.AddScoped<IUserManager, UserManagerService>();
    //
    //     var connectionString = configuration.GetConnectionString("NorthwindDatabase");
    //
    //     services.AddDbContext<ApplicationDbContext>(options =>
    //         options.UseSqlServer(connectionString));
    //
    //     services.AddScoped<ApplicationDbContextInitializer>();
    //
    //
    //
    //
    //     services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    //         .AddEntityFrameworkStores<ApplicationDbContext>()
    //         .AddDefaultTokenProviders();
    //
    //     // NOTE: Need to leave this in otherwise NSWAG throws the following excecption
    //     //---> System.Reflection.ReflectionTypeLoadException: Unable to load one or more of the requested types.
    //     //    Method 'get_ServerSideSessions' in type 'Microsoft.AspNetCore.ApiAuthorization.IdentityServer.ApiAuthorizationDbContext`1' from assembly 'Microsoft.AspNetCore.ApiAuthorization.IdentityServer, Version=8.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60' does not have an implementation.
    //     services.AddControllersWithViews()
    //         .ConfigureApplicationPartManager(apm =>
    //         {
    //             apm.ApplicationParts
    //                 .Remove(apm.ApplicationParts.Single(ap =>
    //                     ap.Name == "Microsoft.AspNetCore.ApiAuthorization.IdentityServer"));
    //         });
    //
    //     var identityServerBuilder = services
    //             .AddIdentityServer(options =>
    //             {
    //                 options.Events.RaiseErrorEvents = true;
    //                 options.Events.RaiseInformationEvents = true;
    //                 options.Events.RaiseFailureEvents = true;
    //                 options.Events.RaiseSuccessEvents = true;
    //
    //                 // see https://docs.duendesoftware.com/identityserver/v5/fundamentals/resources/
    //                 options.EmitStaticAudienceClaim = true;
    //             })
    //             .AddAspNetIdentity<ApplicationUser>()
    //             // .AddConfigurationStore(options =>
    //             // {
    //             //     options.ConfigureDbContext = b =>
    //             //         b.UseSqlServer(connectionString,
    //             //             dbOpts => dbOpts.MigrationsAssembly(typeof(IInfrastructureMarker).Assembly.FullName));
    //             // })
    //             // this is something you will want in production to reduce load on and requests to the DB
    //             //.AddConfigurationStoreCache()
    //             //
    //             // this adds the operational data from DB (codes, tokens, consents)
    //             // .AddOperationalStore(options =>
    //             // {
    //             //     options.ConfigureDbContext = b =>
    //             //         b.UseSqlServer(connectionString,
    //             //             dbOpts => dbOpts.MigrationsAssembly(typeof(IInfrastructureMarker).Assembly.FullName));
    //             // })
    //
    //         //.AddServerSideSessions();
    //         ;
    //
    //
    //
    //     if (environment.IsEnvironment("Test") || environment.IsDevelopment())
    //     {
    //         services.AddTransient<IEmailSender, DebugEmailService>();
    //
    //         var clients = new List<Client>
    //         {
    //             new()
    //             {
    //                 ClientId = "Northwind.IntegrationTests",
    //                 AllowedGrantTypes = { GrantType.ResourceOwnerPassword },
    //                 ClientSecrets = { new Secret("secret".Sha256()) },
    //                 AllowedScopes = { "Northwind.WebUIAPI", "openid", "profile" }
    //             },
    //             new()
    //             {
    //                 ClientId = "test-client",
    //                 ClientName = "Test Client for debugging",
    //                 RequirePkce = false,
    //                 RequireClientSecret = false,
    //                 AlwaysIncludeUserClaimsInIdToken = true,
    //                 AlwaysSendClientClaims = true,
    //                 AllowedGrantTypes = { GrantType.AuthorizationCode },
    //                 AllowOfflineAccess = true,
    //                 AllowedScopes = { "Northwind.WebUIAPI", "openid", "profile", "offline_access" },
    //                 RedirectUris = new[] { "https://localhost:3000/callback" },
    //                 AllowedCorsOrigins = new[] { "https://localhost:3000" }
    //             }
    //         };
    //
    //         identityServerBuilder
    //             .AddInMemoryClients(clients)
    //
    //             //.AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
    //             // {
    //             //     options.Clients.Add(new Client
    //             //     {
    //             //         ClientId = "Northwind.IntegrationTests",
    //             //         AllowedGrantTypes = { GrantType.ResourceOwnerPassword },
    //             //         ClientSecrets = { new Secret("secret".Sha256()) },
    //             //         AllowedScopes = { "Northwind.WebUIAPI", "openid", "profile" }
    //             //     });
    //             //
    //             //     options.Clients.Add(new Client
    //             //     {
    //             //         ClientId = "test-client",
    //             //         ClientName = "Test Client for debugging",
    //             //         RequirePkce = false,
    //             //         RequireClientSecret = false,
    //             //         AlwaysIncludeUserClaimsInIdToken = true,
    //             //         AlwaysSendClientClaims = true,
    //             //         AllowedGrantTypes = { GrantType.AuthorizationCode },
    //             //         AllowOfflineAccess = true,
    //             //         AllowedScopes = { "Northwind.WebUIAPI", "openid", "profile", "offline_access" },
    //             //         RedirectUris = new[] { "https://localhost:3000/callback.html" },
    //             //         AllowedCorsOrigins = new[] { "https://localhost:3000" }
    //             //     });
    //             // })
    //             .AddTestUsers(new List<TestUser>
    //             {
    //                 new()
    //                 {
    //                     SubjectId = "f26da293-02fb-4c90-be75-e4aa51e0bb17",
    //                     Username = "jason@northwind",
    //                     Password = "Northwind1!",
    //                     Claims = new List<Claim> { new Claim(JwtClaimTypes.Email, "jason@northwind") }
    //                 }
    //             });
    //     }
    //     else
    //     {
    //         // TODO DM: Fix
    //         // identityServerBuilder
    //         //     .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();
    //     }
    //
    //     services.AddAuthentication()
    //         .AddIdentityServerJwt();
    // }
}