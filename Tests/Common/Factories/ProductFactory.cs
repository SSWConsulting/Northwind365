using Bogus;
using Northwind.Domain.Products;

namespace Common.Factories;

public static class ProductFactory
{
    private static readonly Faker<Product> Faker = new Faker<Product>().CustomInstantiator(f => Product.Create(
        f.Commerce.ProductName(),
        null,
        null,
        f.Random.Decimal(),
        f.Random.Bool()
    ));

    public static Product Generate() => Faker.Generate();

    public static IEnumerable<Product> Generate(int num) => Faker.Generate(num);
}