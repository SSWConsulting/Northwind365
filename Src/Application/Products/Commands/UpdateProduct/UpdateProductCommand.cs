using Ardalis.Specification.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Common.Exceptions;
using Northwind.Application.Common.Interfaces;
using Northwind.Application.Common.Mappings;
using Northwind.Domain.Products;

namespace Northwind.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand(int ProductId, string ProductName, decimal? UnitPrice, int? SupplierId,
    int? CategoryId, bool Discontinued) : IRequest;

// ReSharper disable once UnusedType.Global
public class UpdateProductCommandHandler(INorthwindDbContext context) : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productId = new ProductId(request.ProductId);
        var entity = await context.Products
            .WithSpecification(new ProductByIdSpec(productId))
            .FirstOrDefaultAsync(cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Product), request.ProductId);

        entity.UpdateProduct(request.ProductName, request.CategoryId.ToCategoryId(), request.SupplierId.ToSupplierId(),
            request.Discontinued);

        await context.SaveChangesAsync(cancellationToken);
    }
}