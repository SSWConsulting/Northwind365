using MediatR;
using Northwind.Application.Common.Interfaces;
using Northwind.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Northwind.Application.Customers.Queries.GetCustomersQueryable
{
    public class GetCustomersQueryable : IRequest<IQueryable<Customer>>
    {
        
    }

    public class GetCustomerQueryableHandler : IRequestHandler<GetCustomersQueryable, IQueryable<Customer>>
    {
        private readonly INorthwindDbContext _northwindDbContext;

        public GetCustomerQueryableHandler(INorthwindDbContext northwindDbContext)
        {
            _northwindDbContext = northwindDbContext;
        }

        public async Task<IQueryable<Customer>> Handle(GetCustomersQueryable request, CancellationToken cancellationToken)
        {
            return _northwindDbContext.Customers;
        }
    }
}
