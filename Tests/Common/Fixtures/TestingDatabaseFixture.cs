using Microsoft.Extensions.DependencyInjection;
using Respawn;
using Respawn.Graph;
using Xunit;

namespace Common.Fixtures;

public class TestingDatabaseFixture : IAsyncLifetime
{
    public const string DatabaseCollectionDefinition = "Database collection";

    private Respawner _checkpoint = default!;

    public WebUiTestFactory Factory { get; }
    public IServiceScopeFactory ScopeFactory { get; private set; } = default!;

    private string ConnectionString => Factory.Database.ConnectionString!;

    // ReSharper disable once ConvertConstructorToMemberInitializers
    public TestingDatabaseFixture()
    {
        Factory = new WebUiTestFactory();
    }

    public async Task InitializeAsync()
    {
        await Factory.Database.InitializeAsync();
        ScopeFactory = Factory.Services.GetRequiredService<IServiceScopeFactory>();
        using var scope = ScopeFactory.CreateScope();

        _checkpoint = await Respawner.CreateAsync(ConnectionString,
            new RespawnerOptions
            {
                // These tables shouldn't be modified from any tests
                TablesToIgnore = new Table[] { "Categories", "Region", "Suppliers", "Shippers", "Territories", },
            });
    }

    public async Task ResetState()
    {
        await _checkpoint.ResetAsync(ConnectionString);
    }

    public async Task DisposeAsync()
    {
        await Factory.Database.DisposeAsync();
    }
}

// [CollectionDefinition(TestingDatabaseFixture.DatabaseCollectionDefinition)]
// public class TestingDatabaseFixtureCollection : ICollectionFixture<TestingDatabaseFixture>
// {
//     // This class has no code, and is never created. Its purpose is simply
//     // to be the place to apply [CollectionDefinition] and all the
//     // ICollectionFixture<> interfaces.
// }