using MediatR;
using Northwind.Application.Common.Interfaces;
using Northwind.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Northwind.Application.Orders.Queries.GetCustomerOrders
{
    public class GetCustomerOrdersQuery : IRequest<IQueryable<Order>>
    {
        public string CustomerId { get; set; }
    }

    public class GetCustomerOrdersQueryHandler : IRequestHandler<GetCustomerOrdersQuery, IQueryable<Order>>
    {
        private readonly INorthwindDbContext _northwindDbContext;

        public GetCustomerOrdersQueryHandler(INorthwindDbContext northwindDbContext)
        {
            _northwindDbContext = northwindDbContext;
        }

        public Task<IQueryable<Order>> Handle(GetCustomerOrdersQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_northwindDbContext.Orders.Where(o => o.CustomerId == request.CustomerId));
        }
    }
}
