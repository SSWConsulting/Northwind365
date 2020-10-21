using MediatR;
using Northwind.Application.Common.Interfaces;
using Northwind.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Northwind.Application.Customers.Commands.CreateGraphCustomer
{
    public class CreateGraphCustomerCommand : IRequest<Customer>
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
    }

    public class CreateGraphCustomerCommandHandler : IRequestHandler<CreateGraphCustomerCommand, Customer>
    {
        private readonly INorthwindDbContext _context;

        public CreateGraphCustomerCommandHandler(INorthwindDbContext northwindDbContext)
        {
            _context = northwindDbContext;
        }

        public async Task<Customer> Handle(CreateGraphCustomerCommand request, CancellationToken cancellationToken)
        {
            var entity = new Customer
            {
                CustomerId = request.Id,
                Address = request.Address,
                City = request.City,
                CompanyName = request.CompanyName,
                ContactName = request.ContactName,
                ContactTitle = request.ContactTitle,
                Country = request.Country,
                Fax = request.Fax,
                Phone = request.Phone,
                PostalCode = request.PostalCode
            };

            _context.Customers.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}
