using AutoMapper;
using MediatR;
using Northwind.Application.Common.Exceptions;
using Northwind.Application.Common.Interfaces;

using System.Threading;
using System.Threading.Tasks;

using Northwind.Domain.Customers;

namespace Northwind.Application.Customers.Queries.GetCustomerDetail;

public record GetCustomerDetailQuery(string Id) : IRequest<CustomerDetailVm>;

public class GetCustomerDetailQueryHandler : IRequestHandler<GetCustomerDetailQuery, CustomerDetailVm>
{
    private readonly INorthwindDbContext _context;
    private readonly IMapper _mapper;

    public GetCustomerDetailQueryHandler(INorthwindDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CustomerDetailVm> Handle(GetCustomerDetailQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers
            .FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Customer), request.Id);
        }

        return _mapper.Map<CustomerDetailVm>(entity);
    }
}