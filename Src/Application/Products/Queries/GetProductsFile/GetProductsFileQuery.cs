using AutoMapper;
using AutoMapper.QueryableExtensions;

using MediatR;

using Microsoft.EntityFrameworkCore;

using Northwind.Application.Common.Interfaces;

namespace Northwind.Application.Products.Queries.GetProductsFile;

public sealed record GetProductsFileQuery : IRequest<ProductsFileVm>;

public sealed class GetProductsFileQueryHandler(INorthwindDbContext context, ICsvBuilder fileBuilder, IMapper mapper,
        IDateTime dateTime)
    : IRequestHandler<GetProductsFileQuery, ProductsFileVm>
{
    public async Task<ProductsFileVm> Handle(GetProductsFileQuery request, CancellationToken cancellationToken)
    {
        var records = await context.Products
            .ProjectTo<ProductRecordDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var fileContent = await fileBuilder.GetCsvBytes(records);

        var vm = new ProductsFileVm
        {
            Content = fileContent,
            ContentType = "text/csv",
            FileName = $"{dateTime.Now:yyyy-MM-dd}-Products.csv"
        };

        return vm;
    }
}