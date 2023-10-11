﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Common.Exceptions;
using Northwind.Application.Common.Interfaces;

using System.Threading;
using System.Threading.Tasks;

using Northwind.Domain.Products;

namespace Northwind.Application.Products.Queries.GetProductDetail;

public class GetProductDetailQuery : IRequest<ProductDetailVm>
{
    public int Id { get; set; }
}

public class GetProductDetailQueryHandler : MediatR.IRequestHandler<GetProductDetailQuery, ProductDetailVm>
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
        var vm = await _context.Products
            .ProjectTo<ProductDetailVm>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(p => p.ProductId == request.Id, cancellationToken);

        if (vm == null)
        {
            throw new NotFoundException(nameof(Product), request.Id);
        }

        return vm;
    }
}