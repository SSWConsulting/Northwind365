using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Northwind.Application.Common.Exceptions;
using Northwind.Application.Common.Interfaces;
using Northwind.Application.Common.Mappings;
using Northwind.Application.Products.Commands.CreateProduct;
using Northwind.Domain.Products;
using Northwind.Domain.Supplying;

namespace Northwind.Application.Products.Commands.UpdateProduct;

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
        {
            throw new NotFoundException(nameof(Product), request.ProductId);
        }

        entity.ProductId = request.ProductId;
        entity.ProductName = request.ProductName;
        entity.CategoryId = request.CategoryId;
        entity.SupplierId = request.SupplierId.ToSupplierId();
        entity.Discontinued = request.Discontinued;

        await _context.SaveChangesAsync(cancellationToken);
    }
}