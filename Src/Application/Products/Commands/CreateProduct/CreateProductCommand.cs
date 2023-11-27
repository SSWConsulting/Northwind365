using MediatR;
using Northwind.Application.Common.Interfaces;
using Northwind.Application.Common.Mappings;
using Northwind.Domain.Products;

namespace Northwind.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(string ProductName, decimal? UnitPrice, int? SupplierId, int? CategoryId,
    bool Discontinued) : IRequest<int>;

// ReSharper disable once UnusedType.Global
public class CreateProductCommandHandler(INorthwindDbContext context) : IRequestHandler<CreateProductCommand, int>
{
    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = Product.Create
        (
            request.ProductName,
            request.CategoryId.ToCategoryId(),
            request.SupplierId.ToSupplierId(),
            request.UnitPrice,
            request.Discontinued
        );

        context.Products.Add(entity);

        await context.SaveChangesAsync(cancellationToken);

        return entity.Id.Value;
    }
}