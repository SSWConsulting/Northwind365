using Bogus;
using Humanizer;
using Northwind.Domain.Common;
using Northwind.Domain.Customers;

namespace Common.Factories;

public static class CustomerFactory
{
    private static readonly Faker<Customer> Faker = new Faker<Customer>().CustomInstantiator(f => Customer.Create(
        new CustomerId(f.Commerce.Ean8()),
        f.Company.CompanyName(0),
        f.Name.FullName(),
        f.Name.JobTitle(),
        Address.Create(
            f.Address.StreetAddress(),
            f.Address.City(),
            f.Address.State(),
            new PostCode(f.Address.ZipCode()),
            new Country(f.Address.Country().Truncate(15))
        ),
        new Phone(f.Phone.PhoneNumber()),
        new Phone(f.Phone.PhoneNumber())
    ));

    public static Customer Generate() => Faker.Generate();

    public static IEnumerable<Customer> Generate(int num) => Faker.Generate(num);
}