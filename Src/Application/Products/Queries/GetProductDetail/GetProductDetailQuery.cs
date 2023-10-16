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
public class GetProductDetailQueryHandler : IRequestHandler<GetProductDetailQuery, ProductDetailVm>
{
    private readonly INorthwindDbContext _context;
    private readonly IMapper _mapper;

    public GetProductDetailQueryHandler(INorthwindDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProductDetailVm> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
    {
        var productId = new ProductId(request.Id);
        var vm = await _context.Products
            .WithSpecification(new ProductByIdSpec(productId))
            .ProjectTo<ProductDetailVm>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (vm == null)
        {
            throw new NotFoundException(nameof(Product), request.Id);
        }

        return vm;
    }
}