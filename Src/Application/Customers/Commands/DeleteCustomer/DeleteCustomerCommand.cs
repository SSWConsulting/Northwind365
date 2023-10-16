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

// ReSharper disable once UnusedType.Global
public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
{
    private readonly INorthwindDbContext _context;

    public DeleteCustomerCommandHandler(INorthwindDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerId = new CustomerId(request.Id);
        var entity = await _context.Customers
            .WithSpecification(new CustomerByIdSpec(customerId))
            .FirstOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Customer), request.Id);
        }

        // TODO: Can this logic be moved to the Domain?
        var hasOrders = _context.Orders
            .WithSpecification(new OrderByCustomerIdSpec(customerId))
            .Any();
        if (hasOrders)
        {
            throw new DeleteFailureException(nameof(Customer), request.Id,
                "There are existing orders associated with this customer.");
        }

        _context.Customers.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}