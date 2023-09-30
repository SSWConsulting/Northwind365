using MediatR;

using Microsoft.EntityFrameworkCore;

using Northwind.Application.Common.Exceptions;
using Northwind.Application.Common.Interfaces;

using System.Threading;
using System.Threading.Tasks;

using Northwind.Domain.Common;
using Northwind.Domain.Customers;

namespace Northwind.Application.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommand : IRequest
{
    public Guid Id { get; set; }
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

    public class Handler : IRequestHandler<UpdateCustomerCommand>
    {
        private readonly INorthwindDbContext _context;

        public Handler(INorthwindDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Customers
                .SingleOrDefaultAsync(c => c.Id == new CustomerId(request.Id), cancellationToken);

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

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}