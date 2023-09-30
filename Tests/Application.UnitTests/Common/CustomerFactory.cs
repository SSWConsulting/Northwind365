﻿using Bogus;

using Northwind.Domain.Customers;

namespace Northwind.Application.UnitTests.Common;

public static class CustomerFactory
{
    private static Faker<Customer> _faker = new Faker<Customer>().CustomInstantiator(f => new Customer(
        //CustomerId = "ALFKI",
        f.Company.CompanyName(0),
        f.Name.FullName(),
        f.Name.JobTitle(),
        f.Address.StreetAddress(),
        f.Address.City(),
        f.Address.State(),
        f.Address.ZipCode(),
        f.Address.Country(),
        f.Phone.PhoneNumber(),
        f.Phone.PhoneNumber()
    ));

    public static Customer Generate() => _faker.Generate();

    public static IEnumerable<Customer> Generate(int num) => _faker.Generate(num);
}