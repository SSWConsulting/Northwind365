using Respawn;
using Xunit;

namespace Northwind.WebUI.IntegrationTests.Common;

public class TestingDatabaseFixture : IAsyncLifetime
{
    public const string DatabaseCollectionDefinition = "Database collection";

    private Respawner _checkpoint = default!;

    public WebUITestFactory Factory { get; }
    public IServiceScopeFactory ScopeFactory { get; private set; } = default!;

    private string ConnectionString => Factory.Database.ConnectionString!;

    // ReSharper disable once ConvertConstructorToMemberInitializers
    public TestingDatabaseFixture()
    {
        Factory = new WebUITestFactory();
    }

    public async Task InitializeAsync()
    {
        await Factory.Database.InitializeAsync();
        ScopeFactory = Factory.Services.GetRequiredService<IServiceScopeFactory>();
        using var scope = ScopeFactory.CreateScope();

        _checkpoint = await Respawner.CreateAsync(ConnectionString);
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

[CollectionDefinition(TestingDatabaseFixture.DatabaseCollectionDefinition)]
public class TestingDatabaseFixtureCollection : ICollectionFixture<TestingDatabaseFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}