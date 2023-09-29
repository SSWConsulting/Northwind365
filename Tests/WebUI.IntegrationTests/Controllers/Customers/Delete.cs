using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Northwind.WebUI.IntegrationTests.Common;
using Xunit;

namespace Northwind.WebUI.IntegrationTests.Controllers.Customers;

public class Delete : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public Delete(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GivenId_ReturnsSuccessStatusCode()
    {
        var client = await _factory.GetAuthenticatedClientAsync();

        var validId = "ALFKI";

        var response = await client.DeleteAsync($"/api/customers/delete/{validId}");

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GivenInvalidId_ReturnsNotFoundStatusCode()
    {
        var client = await _factory.GetAuthenticatedClientAsync();

        var invalidId = "AAAAA";

        var response = await client.DeleteAsync($"/api/customers/delete/{invalidId}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
