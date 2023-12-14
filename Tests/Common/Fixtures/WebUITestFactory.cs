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
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace Common.Fixtures;

public class WebUiTestFactory : WebApplicationFactory<IWebUiMarker>
{
    public DatabaseContainer Database { get; }

    public ITestOutputHelper Output { get; set; } = null!;

    private HttpClient? _authenticatedClient;

    // ReSharper disable once ConvertConstructorToMemberInitializers
    public WebUiTestFactory()
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
    }

    public async Task<HttpClient> GetAuthenticatedClientAsync()
    {
        return await GetAuthenticatedClientAsync("daniel@northwind.com", "Northwind1!");
    }

    private async Task<HttpClient> GetAuthenticatedClientAsync(string userName, string password)
    {
        if (_authenticatedClient is not null)
            return _authenticatedClient;

        var client = CreateClient();
        await Register(userName, password, client);
        var loginResponseContent = await Login(userName, password, client);
        SetAuthHeader(client, loginResponseContent);

        _authenticatedClient = client;

        return _authenticatedClient;
    }

    private static void SetAuthHeader(HttpClient client, LoginResponse? loginResponseContent)
    {
        ArgumentNullException.ThrowIfNull(loginResponseContent);

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", loginResponseContent.AccessToken);
    }

    private static async Task<LoginResponse?> Login(string userName, string password, HttpClient client)
    {
        var loginResponse = await client.PostAsJsonAsync("/api/auth/login", new { Email = userName, Password = password });
        loginResponse.EnsureSuccessStatusCode();

        var loginResponseContent = await loginResponse.Content.ReadFromJsonAsync<LoginResponse>();
        if (loginResponseContent is null)
            throw new NullReferenceException("Not able to generate access token");

        return loginResponseContent;
    }

    private static async Task Register(string userName, string password, HttpClient client)
    {
        var registerResponse = await client.PostAsJsonAsync("/api/auth/register", new { Email = userName, Password = password });
        registerResponse.EnsureSuccessStatusCode();
    }
}

public record LoginResponse(string TokenType, string AccessToken, int ExpiresIn, string RefreshToken);

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