﻿using FluentValidation;

namespace Northwind.Application.Customers.Commands.CreateCustomer;

// ReSharper disable once UnusedType.Global
public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Address).MaximumLength(60);
        RuleFor(x => x.City).MaximumLength(15);
        RuleFor(x => x.CompanyName).MaximumLength(40).NotEmpty();
        RuleFor(x => x.ContactName).MaximumLength(30);
        RuleFor(x => x.ContactTitle).MaximumLength(30);
        //RuleFor(x => x.Country).MaximumLength(15);
        //RuleFor(x => x.Fax).MaximumLength(24);
        //RuleFor(x => x.Phone).MaximumLength(24);
        RuleFor(x => x.PostalCode).NotEmpty();
        RuleFor(x => x.Region).MaximumLength(15);
    }
}