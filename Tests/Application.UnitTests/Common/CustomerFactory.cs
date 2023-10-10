using Bogus;

using Northwind.Domain.Common;
using Northwind.Domain.Customers;

namespace Northwind.Application.UnitTests.Common;

public static class CustomerFactory
{
    private static readonly Faker<Customer> Faker = new Faker<Customer>().CustomInstantiator(f => Customer.Create(
        f.Company.CompanyName(0),
        f.Name.FullName(),
        f.Name.JobTitle(),
        new Address(
            f.Address.StreetAddress(),
            f.Address.City(),
            f.Address.State(),
            f.Address.ZipCode(),
            f.Address.Country()
        ),
        f.Phone.PhoneNumber(),
        f.Phone.PhoneNumber()
    ));

    public static Customer Generate() => Faker.Generate();

    public static IEnumerable<Customer> Generate(int num) => Faker.Generate(num);
}