using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Common.Interfaces;

namespace Northwind.Application.Customers.Queries.GetCustomersCsv;

public sealed record GetCustomersCsvQuery : IRequest<CustomersCsvVm>;

public sealed class GetCustomersCsvQueryHandler(
    INorthwindDbContext context,
    IMapper mapper,
    IDateTime dateTime,
    ICsvBuilder csvBuilder) : IRequestHandler<GetCustomersCsvQuery, CustomersCsvVm>
{
    public async Task<CustomersCsvVm> Handle(GetCustomersCsvQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<CustomerCsvLookupDto> customers = await context.Customers
            .ProjectTo<CustomerCsvLookupDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        byte[] data = await csvBuilder.GetCsvBytes(customers);
        
        return new CustomersCsvVm
        {
            Data = data,
            FileName = $"{dateTime.Now:yyyy-MM-dd}-Products.csv",
        };
    }
}