using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using Northwind.Application.Common.Interfaces;
using Northwind.Application.Common.Mappings;
using Northwind.Domain.Products;

namespace Northwind.Application.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<int>
{
    public string ProductName { get; set; }

    public decimal? UnitPrice { get; set; }

    public Guid? SupplierId { get; set; }

    public int? CategoryId { get; set; }

    public bool Discontinued { get; set; }
}

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