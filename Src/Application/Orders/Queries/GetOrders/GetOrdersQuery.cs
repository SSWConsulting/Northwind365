using MediatR;
using Northwind.Application.Common.Interfaces;
using Northwind.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Northwind.Application.Orders.Queries.GetOrders
{
    public class GetOrdersQuery : IRequest<IQueryable<Order>>
    {
        
    }

    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IQueryable<Order>>
    {
        private readonly INorthwindDbContext _northwindDbContext;

        public GetOrdersQueryHandler(INorthwindDbContext northwindDbContext)
        {
            _northwindDbContext = northwindDbContext;
        }

        public async Task<IQueryable<Order>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            return _northwindDbContext.Orders;
        }
    }
}
