using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Northwind.Infrastructure.Identity;
using Northwind.Infrastructure.Persistence;
using Northwind.WebUI;

namespace Northwind.Infrastructure.IntegrationTests.TestHelpers;

internal class InfrastructureTestFactory : WebApplicationFactory<IWebUiMarker>
{
    public DatabaseContainer Database { get; }

    // ReSharper disable once ConvertConstructorToMemberInitializers
    public InfrastructureTestFactory()
    {
        Database = new DatabaseContainer();
    }

    protected override void ConfigureWebHost(IWebHostBuilder webHostBuilder)
    {
        webHostBuilder.ConfigureTestServices(services => DbContextExt
            .ReplaceDbContext<ApplicationDbContext>(services, Database)
            .ReplaceDbContext<NorthwindDbContext>(Database));
    }
}