using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Common.Interfaces;
using Northwind.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Northwind.Application.Customers.Queries.GetCustomer
{
    public class GetCustomerQuery : IRequest<Customer>
    {
        public string CustomerId { get; set; }
    }

    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, Customer>
    {
        private readonly INorthwindDbContext _northwindDbContext;

        public GetCustomerQueryHandler(INorthwindDbContext northwindDbContext)
        {
            _northwindDbContext = northwindDbContext;
        }

        public async Task<Customer> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            return await _northwindDbContext.Customers
                .Where(c => c.CustomerId == request.CustomerId)
                .SingleAsync(cancellationToken);
        }
    }
}
