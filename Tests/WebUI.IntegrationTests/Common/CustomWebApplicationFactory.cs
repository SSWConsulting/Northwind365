using IdentityModel.Client;
using Meziantou.Extensions.Logging.Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Common.Interfaces;
using Northwind.Infrastructure.Persistence;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Northwind.WebUI.IntegrationTests.Common;

public class CustomWebApplicationFactory : WebApplicationFactory<IWebUiMarker>
{
    public ITestOutputHelper Output { get; set; }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(x =>
        {
            x.ClearProviders();
            x.AddFilter(level => level >= LogLevel.Information);
            x.Services.AddSingleton<ILoggerProvider>(new XUnitLoggerProvider(Output));
        });

        builder
            .ConfigureServices(services =>
            {
                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                // Add a database context using an in-memory 
                // database for testing.
                services.AddDbContext<NorthwindDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                services.AddScoped<INorthwindDbContext>(provider => provider.GetService<NorthwindDbContext>());

                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory>>();

                try
                {
                    // Initialise and seed database
                    var initializer = scope.ServiceProvider.GetRequiredService<NorthwindDbContextInitializer>();
                    initializer.InitializeAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                    // TODO: Consider adding profiles to specify different amounts of data to seed
                    initializer.SeedAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                    ;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred seeding the database with test messages. Error: {Message}",
                        ex.Message);
                }
            })
            .UseEnvironment("Test");
    }

    public async Task AddEntityAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class
    {
        await using var dbContext = Services.GetRequiredService<NorthwindDbContext>();
        dbContext.Set<T>().Add(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public HttpClient GetAnonymousClient()
    {
        return CreateClient();
    }

    public async Task<HttpClient> GetAuthenticatedClientAsync()
    {
        return await GetAuthenticatedClientAsync("jason@northwind", "Northwind1!");
    }

    public async Task<HttpClient> GetAuthenticatedClientAsync(string userName, string password)
    {
        var client = CreateClient();

        var token = await GetAccessTokenAsync(client, userName, password);

        client.SetBearerToken(token);

        return client;
    }

    private async Task<string> GetAccessTokenAsync(HttpClient client, string userName, string password)
    {
        var disco = await client.GetDiscoveryDocumentAsync();

        if (disco.IsError)
        {
            throw new Exception(disco.Error);
        }

        var response = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "Northwind.IntegrationTests",
            ClientSecret = "secret",
            Scope = "Northwind.WebUIAPI openid profile",
            UserName = userName,
            Password = password
        });

        if (response.IsError)
        {
            throw new Exception(response.Error);
        }

        return response.AccessToken;
    }
}