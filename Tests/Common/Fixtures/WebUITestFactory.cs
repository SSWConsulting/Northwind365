using IdentityModel.Client;
using Meziantou.Extensions.Logging.Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Northwind.Infrastructure.Identity;
using Northwind.Infrastructure.Persistence;
using Northwind.WebUI;
using Xunit.Abstractions;

namespace Common.Fixtures;

public class WebUITestFactory : WebApplicationFactory<IWebUiMarker>
{
    public DatabaseContainer Database { get; }

    public ITestOutputHelper Output { get; set; }

    // ReSharper disable once ConvertConstructorToMemberInitializers
    public WebUITestFactory()
    {
        Database = new DatabaseContainer();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureLogging(x =>
        {
            x.ClearProviders();
            x.AddFilter(level => level >= LogLevel.Information);
            x.Services.AddSingleton<ILoggerProvider>(new XUnitLoggerProvider(Output));
        });

        builder.ConfigureTestServices(services => services
            .ReplaceDbContext<ApplicationDbContext>(Database)
            .ReplaceDbContext<NorthwindDbContext>(Database));

        // TODO: Is this needed?
        //builder.UseEnvironment("Test");
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