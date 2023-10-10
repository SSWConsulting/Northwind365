using Bogus;
using Northwind.Domain.Products;

namespace Northwind.Application.UnitTests.Common;

public static class ProductFactory
{
    private static Faker<Product> _faker = new Faker<Product>().CustomInstantiator(f => Product.Create(
        f.Commerce.ProductName(),
        null,
        null,
        f.Random.Decimal(),
        f.Random.Bool()
    ));

    public static Product Generate() => _faker.Generate();

    public static IEnumerable<Product> Generate(int num) => _faker.Generate(num);
}