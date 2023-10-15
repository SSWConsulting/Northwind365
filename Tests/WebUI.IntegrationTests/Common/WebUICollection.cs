using Xunit;

namespace Northwind.WebUI.IntegrationTests.Common;

[CollectionDefinition(Definition)]
public sealed class WebUICollection : ICollectionFixture<CustomWebApplicationFactory>
{
    public const string Definition = nameof(WebUICollection);
}