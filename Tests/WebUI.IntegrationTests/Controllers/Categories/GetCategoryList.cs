using Northwind.WebUI.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.WebUI.IntegrationTests.Controllers.Categories;

//[Collection(WebUICollection.Definition)]
public class GetCategoryList : IntegrationTestBase
{
    public GetCategoryList(TestingDatabaseFixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
    public async Task ReturnsSuccessResult()
    {
        var client = await GetAuthenticatedClientAsync();

        var response = await client.GetAsync("/api/categories");

        response.EnsureSuccessStatusCode();
    }
}