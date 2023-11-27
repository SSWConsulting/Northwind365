using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Common.Exceptions;
using Northwind.Application.Common.Interfaces;

using System.Threading;
using System.Threading.Tasks;

using Northwind.Domain.Products;

namespace Northwind.Application.Products.Queries.GetProductDetail;

public record GetProductDetailQuery(int Id) : IRequest<ProductDetailVm>;

// ReSharper disable once UnusedType.Global
public class GetProductDetailQueryHandler(INorthwindDbContext context, IMapper mapper) : IRequestHandler<GetProductDetailQuery, ProductDetailVm>
{
    public async Task<ProductDetailVm> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
    {
        var productId = new ProductId(request.Id);
        var vm = await context.Products
            .WithSpecification(new ProductByIdSpec(productId))
            .ProjectTo<ProductDetailVm>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (vm == null)
        {
            throw new NotFoundException(nameof(Product), request.Id);
        }

        return vm;
    }
}