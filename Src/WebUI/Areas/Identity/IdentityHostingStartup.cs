using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Northwind.WebUI.Areas.Identity.IdentityHostingStartup))]
namespace Northwind.WebUI.Areas.Identity;

public class IdentityHostingStartup : IHostingStartup
{
    public void Configure(IWebHostBuilder builder)
    {
        builder.ConfigureServices((context, services) => { });
    }
}