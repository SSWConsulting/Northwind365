using FluentValidation;
using FluentValidation.Validators;

namespace Northwind.Application.Customers.Commands.UpdateCustomer;

// ReSharper disable once UnusedType.Global
public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Address).MaximumLength(60);
        RuleFor(x => x.City).MaximumLength(15);
        RuleFor(x => x.CompanyName).MaximumLength(40).NotEmpty();
        RuleFor(x => x.ContactName).MaximumLength(30);
        RuleFor(x => x.ContactTitle).MaximumLength(30);
        RuleFor(x => x.Fax).NotEmpty();
        RuleFor(x => x.Phone).NotEmpty();
        RuleFor(x => x.Region).MaximumLength(15);
    }
}