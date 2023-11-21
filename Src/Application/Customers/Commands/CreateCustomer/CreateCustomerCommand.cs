using MediatR;
using Northwind.Application.Common.Interfaces;
using Northwind.Domain.Common;
using Northwind.Domain.Customers;

namespace Northwind.Application.Customers.Commands.CreateCustomer;

public record CreateCustomerCommand(string Id, string Address, string City, string CompanyName, string ContactName,
    string ContactTitle, string Country, string Fax, string Phone, string PostalCode, string Region) : IRequest;

public class CreateCustomerCommandHandler(INorthwindDbContext context) : IRequestHandler<CreateCustomerCommand>
{
    public async Task Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = Customer.Create
        (
            new CustomerId(request.Id),
            request.CompanyName,
            request.ContactName,
            request.ContactTitle,
            Address.Create(
                request.Address,
                request.City,
                request.Region,
                new PostCode(request.PostalCode),
                new Country(request.Country)
            ),
            new Phone(request.Fax),
            new Phone(request.Phone)
        );

        context.Customers.Add(entity);

        await context.SaveChangesAsync(cancellationToken);
    }
}