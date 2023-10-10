using System.Diagnostics.CodeAnalysis;

using MediatR;

using Northwind.Application.Common.Exceptions;
using Northwind.Application.Common.Interfaces;
using Northwind.Application.Common.Mappings;
using Northwind.Domain.Products;

namespace Northwind.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest
{
    public int ProductId { get; set; }

    public string ProductName { get; set; }

    public decimal? UnitPrice { get; set; }

    public int? SupplierId { get; set; }

    public int? CategoryId { get; set; }

    public bool Discontinued { get; set; }
}

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly INorthwindDbContext _context;

    public UpdateProductCommandHandler(INorthwindDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Products.FindAsync(request.ProductId);

        if (entity == null)
            throw new NotFoundException(nameof(Product), request.ProductId);

        //entity.Id = request.ProductId.ToProductId();
        entity.UpdateProduct(request.ProductName, request.CategoryId.ToCategoryId(), request.SupplierId.ToSupplierId(),
            request.Discontinued);

        await _context.SaveChangesAsync(cancellationToken);
    }
}