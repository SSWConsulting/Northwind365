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

        services.AddScoped<INorthwindDbContext>(provider => provider.GetRequiredService<NorthwindDbContext>());
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

        services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        // TODO DM: Is this needed?
        services.AddControllersWithViews()
            .ConfigureApplicationPartManager(apm =>
            {
                apm.ApplicationParts
                    .Remove(apm.ApplicationParts.Single(ap =>
                        ap.Name == "Microsoft.AspNetCore.ApiAuthorization.IdentityServer"));
            });

        var identityServerBuilder = services
                .AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;

                    // see https://docs.duendesoftware.com/identityserver/v5/fundamentals/resources/
                    options.EmitStaticAudienceClaim = true;
                })
                .AddAspNetIdentity<ApplicationUser>()
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

            //.AddServerSideSessions();
            ;

        List<Client> clients = new List<Client>
        {
            new Client
            {
                ClientId = "Northwind.IntegrationTests",
                AllowedGrantTypes = { GrantType.ResourceOwnerPassword },
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes = { "Northwind.WebUIAPI", "openid", "profile" }
            },
            new Client
            {
                ClientId = "test-client",
                ClientName = "Test Client for debugging",
                RequirePkce = false,
                RequireClientSecret = false,
                AlwaysIncludeUserClaimsInIdToken = true,
                AlwaysSendClientClaims = true,
                AllowedGrantTypes = { GrantType.AuthorizationCode },
                AllowOfflineAccess = true,
                AllowedScopes = { "Northwind.WebUIAPI", "openid", "profile", "offline_access" },
                RedirectUris = new[] { "https://localhost:3000/callback.html" },
                AllowedCorsOrigins = new[] { "https://localhost:3000" }
            }
        };

        if (environment.IsEnvironment("Test") || environment.IsDevelopment())
        {
            services.AddTransient<IEmailSender, DebugEmailService>();

            identityServerBuilder
                .AddInMemoryClients(clients)

                //.AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
                // .AddAspNetIdentity<ApplicationUser>(options =>
                // {
                //     options.Clients.Add(new Client
                //     {
                //         ClientId = "Northwind.IntegrationTests",
                //         AllowedGrantTypes = { GrantType.ResourceOwnerPassword },
                //         ClientSecrets = { new Secret("secret".Sha256()) },
                //         AllowedScopes = { "Northwind.WebUIAPI", "openid", "profile" }
                //     });
                //
                //     options.Clients.Add(new Client
                //     {
                //         ClientId = "test-client",
                //         ClientName = "Test Client for debugging",
                //         RequirePkce = false,
                //         RequireClientSecret = false,
                //         AlwaysIncludeUserClaimsInIdToken = true,
                //         AlwaysSendClientClaims = true,
                //         AllowedGrantTypes = { GrantType.AuthorizationCode },
                //         AllowOfflineAccess = true,
                //         AllowedScopes = { "Northwind.WebUIAPI", "openid", "profile", "offline_access" },
                //         RedirectUris = new[] { "https://localhost:3000/callback.html" },
                //         AllowedCorsOrigins = new[] { "https://localhost:3000" }
                //     });
                // })
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

public class DebugEmailService : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        Console.WriteLine($"MOCK EMAIL:{subject}");
        Console.WriteLine(htmlMessage);

        return Task.CompletedTask;
    }
}

// public class DebugEmailService : IEmailSender<ApplicationUser>
// {
//     public Task SendEmailAsync(string email, string subject, string htmlMessage)
//     {
//         Console.WriteLine($"MOCK EMAIL:{subject}");
//         Console.WriteLine(htmlMessage);
//
//         return Task.CompletedTask;
//     }
//
//     public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
//     {
//         Console.WriteLine($"MOCK EMAIL:{email}");
//         Console.WriteLine(confirmationLink);
//
//         return Task.CompletedTask;
//     }
//
//     public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
//     {
//         Console.WriteLine($"MOCK EMAIL:{email}");
//         Console.WriteLine(resetLink);
//
//         return Task.CompletedTask;
//     }
//
//     public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
//     {
//         Console.WriteLine($"MOCK EMAIL:{email}");
//         Console.WriteLine(resetCode);
//
//         return Task.CompletedTask;
//     }
// }