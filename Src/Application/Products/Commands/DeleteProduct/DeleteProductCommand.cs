using Ardalis.Specification.EntityFrameworkCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Common.Exceptions;
using Northwind.Application.Common.Interfaces;
using Northwind.Domain.Orders;
using Northwind.Domain.Products;

namespace Northwind.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(int Id) : IRequest;

// ReSharper disable once UnusedType.Global
public class DeleteProductCommandHandler(INorthwindDbContext context) : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var productId = new ProductId(request.Id);
        var entity = await context.Products
            .WithSpecification(new ProductByIdSpec(productId))
            .FirstOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Product), request.Id);
        }

        var hasOrders = await context.OrderDetails
            .WithSpecification(new OrderDetailByProductIdSpec(productId))
            .AnyAsync(cancellationToken);
        if (hasOrders)
        {
            // TODO: Add functional test for this behaviour.
            throw new DeleteFailureException(nameof(Product), request.Id,
                "There are existing orders associated with this product.");
        }

        context.Products.Remove(entity);

        await context.SaveChangesAsync(cancellationToken);
    }
}