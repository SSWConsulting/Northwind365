using MediatR;
using Northwind.Application.Common.Interfaces;
using Northwind.Application.Common.Mappings;
using Northwind.Domain.Products;

namespace Northwind.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(string ProductName, decimal? UnitPrice, int? SupplierId, int? CategoryId,
    bool Discontinued) : IRequest<int>;

// ReSharper disable once UnusedType.Global
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly INorthwindDbContext _context;

    public CreateProductCommandHandler(INorthwindDbContext context)
    {
        _context = context;
    }

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

        _context.Products.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id.Value;
    }
}