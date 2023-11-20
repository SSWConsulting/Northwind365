using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Northwind.Application.Customers.Queries.GetCustomersList;

public record GetCustomersListQuery : IRequest<CustomersListVm>;

public class GetCustomersListQueryHandler(INorthwindDbContext context, IMapper mapper) : IRequestHandler<GetCustomersListQuery, CustomersListVm>
{
    public async Task<CustomersListVm> Handle(GetCustomersListQuery request, CancellationToken cancellationToken)
    {
        var customers = await context.Customers
            .ProjectTo<CustomerLookupDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var vm = new CustomersListVm
        {
            Customers = customers
        };

        return vm;
    }
}