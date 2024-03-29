using Testcontainers.SqlEdge;

namespace Common.Fixtures;

public class DatabaseContainer
{
    private readonly SqlEdgeContainer? _container;

    // ReSharper disable once ConvertConstructorToMemberInitializers
    public DatabaseContainer()
    {
        _container = new SqlEdgeBuilder()
            .WithName($"Northwind365-{Guid.NewGuid()}")
            .WithPassword("sqledge!Strong")
            .WithAutoRemove(true)
            .Build();
    }

    public string? ConnectionString { get; private set; }

    public async Task InitializeAsync()
    {
        if (_container != null)
        {
            await _container.StartAsync();
            ConnectionString = _container.GetConnectionString();
        }
    }

    public Task DisposeAsync() => _container?.StopAsync() ?? Task.CompletedTask;
}