using Xunit;

namespace Northwind.WebUI.IntegrationTests.Common;

[CollectionDefinition(Definition)]
public sealed class WebUICollection : ICollectionFixture<CustomWebApplicationFactoryV2>
{
    public const string Definition = nameof(WebUICollection);
}