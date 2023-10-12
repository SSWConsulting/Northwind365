using MediatR;
using Northwind.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Northwind.Domain.Common;
using Northwind.Domain.Customers;

namespace Northwind.Application.Customers.Commands.CreateCustomer;

public record CreateCustomerCommand(string Id, string Address, string City, string CompanyName, string ContactName,
    string ContactTitle, string Country, string Fax, string Phone, string PostalCode, string Region) : IRequest;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand>
{
    private readonly INorthwindDbContext _context;
    private readonly IMediator _mediator;

    public CreateCustomerCommandHandler(INorthwindDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = Customer.Create
        (
            new CustomerId(request.Id),
            request.CompanyName,
            request.ContactName,
            request.ContactTitle,
            new Address(
                request.Address,
                request.City,
                request.Region,
                request.PostalCode,
                request.Country
            ),
            request.Fax,
            request.Phone
        );

        _context.Customers.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        await _mediator.Publish(new CustomerCreated { CustomerId = entity.Id }, cancellationToken);
    }
}