using Ardalis.Specification.EntityFrameworkCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Common.Exceptions;
using Northwind.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Northwind.Domain.Customers;
using Northwind.Domain.Orders;

namespace Northwind.Application.Customers.Commands.DeleteCustomer;

public record DeleteCustomerCommand(string Id) : IRequest;

public class DeleteCustomerCommandHandler(INorthwindDbContext context) : IRequestHandler<DeleteCustomerCommand>
{
    public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerId = new CustomerId(request.Id);
        var entity = await context.Customers
            .WithSpecification(new CustomerByIdSpec(customerId))
            .FirstOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Customer), request.Id);
        }

        // TODO: Can this logic be moved to the Domain?
        var hasOrders = await context.Orders
            .WithSpecification(new OrderByCustomerIdSpec(customerId))
            .AnyAsync(cancellationToken: cancellationToken);
        if (hasOrders)
        {
            throw new DeleteFailureException(nameof(Customer), request.Id,
                "There are existing orders associated with this customer.");
        }

        context.Customers.Remove(entity);

        await context.SaveChangesAsync(cancellationToken);
    }
}