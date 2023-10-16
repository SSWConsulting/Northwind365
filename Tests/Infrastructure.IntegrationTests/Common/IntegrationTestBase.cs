using Common.Fixtures;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Infrastructure.Persistence;

namespace Northwind.Infrastructure.IntegrationTests.Common;

[Collection(TestingDatabaseFixture.DatabaseCollectionDefinition)]
public abstract class IntegrationTestBase : IAsyncLifetime
{
    private readonly IServiceScope _scope;

    protected TestingDatabaseFixture Fixture { get; }
    //protected IMediator Mediator { get; }
    protected NorthwindDbContext Context { get; }


    public IntegrationTestBase(TestingDatabaseFixture fixture)
    {
        Fixture = fixture;

        _scope = Fixture.ScopeFactory.CreateScope();
        //Mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        Context = _scope.ServiceProvider.GetRequiredService<NorthwindDbContext>();
    }

    public async Task InitializeAsync()
    {
        await Fixture.ResetState();
    }

    public Task DisposeAsync()
    {
        _scope.Dispose();
        return Task.CompletedTask;
    }
}

[CollectionDefinition(TestingDatabaseFixture.DatabaseCollectionDefinition)]
public class TestingDatabaseFixtureCollection : ICollectionFixture<TestingDatabaseFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}