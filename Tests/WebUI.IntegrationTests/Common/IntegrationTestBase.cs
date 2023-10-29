using Common.Fixtures;
using MediatR;
using Northwind.Infrastructure.Persistence;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.WebUI.IntegrationTests;

[Collection(TestingDatabaseFixture.DatabaseCollectionDefinition)]
public abstract class IntegrationTestBase : IAsyncLifetime
{
    private readonly IServiceScope _scope;

    protected TestingDatabaseFixture Fixture { get; }
    protected IMediator Mediator { get; }
    protected NorthwindDbContext Context { get; }

    public IntegrationTestBase(TestingDatabaseFixture fixture, ITestOutputHelper output)
    {
        Fixture = fixture;
        Fixture.Factory.Output = output;

        _scope = Fixture.ScopeFactory.CreateScope();
        Mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        Context = _scope.ServiceProvider.GetRequiredService<NorthwindDbContext>();
    }

    public async Task InitializeAsync()
    {
        await Fixture.ResetState();
    }

    public async Task AddEntityAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class
    {
        Context.Set<T>().Add(entity);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public Task DisposeAsync()
    {
        _scope.Dispose();
        return Task.CompletedTask;
    }

    public async Task<HttpClient> GetAuthenticatedClientAsync()
    {
        return await Fixture.Factory.GetAuthenticatedClientAsync();
    }
}

[CollectionDefinition(TestingDatabaseFixture.DatabaseCollectionDefinition)]
public class TestingDatabaseFixtureCollection : ICollectionFixture<TestingDatabaseFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}