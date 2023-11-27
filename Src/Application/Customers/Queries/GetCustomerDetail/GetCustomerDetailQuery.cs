using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Common.Exceptions;
using Northwind.Application.Common.Interfaces;
using Northwind.Domain.Customers;

namespace Northwind.Application.Customers.Queries.GetCustomerDetail;

public record GetCustomerDetailQuery(string Id) : IRequest<CustomerDetailVm>;

public class GetCustomerDetailQueryHandler(INorthwindDbContext context, IMapper mapper) : IRequestHandler<GetCustomerDetailQuery, CustomerDetailVm>
{
    public async Task<CustomerDetailVm> Handle(GetCustomerDetailQuery request, CancellationToken cancellationToken)
    {
        var customerId = new CustomerId(request.Id);
        var entity = await context.Customers
            .WithSpecification(new CustomerByIdSpec(customerId))
            .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Customer), request.Id);
        }

        return mapper.Map<CustomerDetailVm>(entity);
    }
}