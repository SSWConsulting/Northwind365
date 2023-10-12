using Duende.IdentityServer.EntityFramework.Entities;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Persistence;
using Respawn;

namespace Northwind.Infrastructure.IntegrationTests.TestHelpers;

public class TestingDatabaseFixture : IAsyncLifetime
{
    public const string DatabaseCollectionDefinition = "Database collection";

    private readonly IntegrationTestWebApplicationFactory _factory;
    private Respawner _checkpoint = default!;

    public IServiceScopeFactory ScopeFactory { get; private set; } = default!;

    private string ConnectionString => _factory.Database.ConnectionString!;

    // ReSharper disable once ConvertConstructorToMemberInitializers
    public TestingDatabaseFixture()
    {
        _factory = new IntegrationTestWebApplicationFactory();
    }

    public async Task InitializeAsync()
    {
        await _factory.Database.InitializeAsync();
        ScopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        using var scope = ScopeFactory.CreateScope();

        _checkpoint = await Respawner.CreateAsync(ConnectionString);
    }

    public async Task DisposeAsync()
    {
        await _factory.Database.DisposeAsync();
    }

    public async Task ResetState()
    {
        await _checkpoint.ResetAsync(ConnectionString);
    }
}

[CollectionDefinition(TestingDatabaseFixture.DatabaseCollectionDefinition)]
public class TestingDatabaseFixtureCollection : ICollectionFixture<TestingDatabaseFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}