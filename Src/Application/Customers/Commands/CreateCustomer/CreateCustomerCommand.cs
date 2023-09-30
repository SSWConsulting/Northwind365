using MediatR;
using Northwind.Application.Common.Interfaces;

using System.Threading;
using System.Threading.Tasks;

using Northwind.Domain.Customers;

namespace Northwind.Application.Customers.Commands.CreateCustomer;

public class CreateCustomerCommand : IRequest
{
    public string Id { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string CompanyName { get; set; }

    public string ContactName { get; set; }

    public string ContactTitle { get; set; }

    public string Country { get; set; }

    public string Fax { get; set; }

    public string Phone { get; set; }

    public string PostalCode { get; set; }

    public string Region { get; set; }

    public class Handler : IRequestHandler<CreateCustomerCommand>
    {
        private readonly INorthwindDbContext _context;
        private readonly IMediator _mediator;

        public Handler(INorthwindDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var entity = new Customer
            (
                request.Id,
                request.Address,
                request.City,
                request.CompanyName,
                request.ContactName,
                request.ContactTitle,
                request.Country,
                request.Fax,
                request.Phone,
                request.PostalCode
            );

            _context.Customers.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new CustomerCreated { CustomerId = entity.Id }, cancellationToken);
        }
    }
}