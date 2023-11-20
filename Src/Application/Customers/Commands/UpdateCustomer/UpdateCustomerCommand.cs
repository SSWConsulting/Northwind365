using Ardalis.Specification.EntityFrameworkCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Common.Exceptions;
using Northwind.Application.Common.Interfaces;
using Northwind.Domain.Common;
using Northwind.Domain.Customers;

namespace Northwind.Application.Customers.Commands.UpdateCustomer;

public record UpdateCustomerCommand(string Id, string Address, string City, string CompanyName, string ContactName,
    string ContactTitle, string Country, string Fax, string Phone, string PostalCode, string Region) : IRequest;

// ReSharper disable once UnusedType.Global
public class UpdateCustomerCommandHandler(INorthwindDbContext context) : IRequestHandler<UpdateCustomerCommand>
{
    public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerId = new CustomerId(request.Id);
        var entity = await context.Customers
            .WithSpecification(new CustomerByIdSpec(customerId))
            .FirstOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Customer), request.Id);
        }

        entity.UpdateAddress(new Address(request.Address, request.City, request.Region, request.PostalCode,
            request.Country));
        entity.UpdateContact(request.ContactName, request.ContactTitle);
        entity.UpdatePhone(request.Phone);
        entity.UpdateFax(request.Fax);
        entity.UpdateCompanyName(request.CompanyName);

        await context.SaveChangesAsync(cancellationToken);
    }
}