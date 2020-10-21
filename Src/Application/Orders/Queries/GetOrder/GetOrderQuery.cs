using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Common.Interfaces;
using Northwind.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Northwind.Application.Orders.Queries.GetOrder
{
    public class GetOrderQuery : IRequest<Order>
    {
        public int OrderId { get; set; }
    }

    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Order>
    {
        private readonly INorthwindDbContext _northwindDbContext;

        public GetOrderQueryHandler(INorthwindDbContext northwindDbContext)
        {
            _northwindDbContext = northwindDbContext;
        }

        public async Task<Order> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            return await _northwindDbContext.Orders
                .Where(o => o.OrderId == request.OrderId)
                .SingleAsync(cancellationToken);
        }
    }
}
