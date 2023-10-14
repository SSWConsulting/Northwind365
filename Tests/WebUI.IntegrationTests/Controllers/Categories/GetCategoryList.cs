using System.Threading.Tasks;
using Northwind.WebUI.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.WebUI.IntegrationTests.Controllers.Categories;

[Collection(WebUICollection.Definition)]
public class GetCategoryList
{
    private readonly CustomWebApplicationFactory _factory;

    public GetCategoryList(CustomWebApplicationFactory factory, ITestOutputHelper output)
    {
        _factory = factory;
        _factory.Output = output;
    }

    [Fact]
    public async Task ReturnsSuccessResult()
    {
        var client = await _factory.GetAuthenticatedClientAsync();

        var response = await client.GetAsync("/api/categories");

        response.EnsureSuccessStatusCode();
    }
}