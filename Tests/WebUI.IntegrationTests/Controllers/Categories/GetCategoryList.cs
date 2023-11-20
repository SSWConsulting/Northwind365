using Common.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace Northwind.WebUI.IntegrationTests.Controllers.Categories;

public class GetCategoryList(TestingDatabaseFixture fixture, ITestOutputHelper output) : IntegrationTestBase(fixture, output)
{
    [Fact]
    public async Task ReturnsSuccessResult()
    {
        var client = await GetAuthenticatedClientAsync();

        var response = await client.GetAsync("/api/categories");

        response.EnsureSuccessStatusCode();
    }
}