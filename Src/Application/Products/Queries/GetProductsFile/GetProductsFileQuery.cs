using AutoMapper;
using AutoMapper.QueryableExtensions;

using MediatR;

using Microsoft.EntityFrameworkCore;

using Northwind.Application.Common.Interfaces;

namespace Northwind.Application.Products.Queries.GetProductsFile;

public record GetProductsFileQuery : IRequest<ProductsFileVm>;

// ReSharper disable once UnusedType.Global
public class GetProductsFileQueryHandler(INorthwindDbContext context, ICsvFileBuilder fileBuilder, IMapper mapper,
        IDateTime dateTime)
    : IRequestHandler<GetProductsFileQuery, ProductsFileVm>
{
    public async Task<ProductsFileVm> Handle(GetProductsFileQuery request, CancellationToken cancellationToken)
    {
        var records = await context.Products
            .ProjectTo<ProductRecordDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var fileContent = fileBuilder.BuildProductsFile(records);

        var vm = new ProductsFileVm
        {
            Content = fileContent,
            ContentType = "text/csv",
            FileName = $"{dateTime.Now:yyyy-MM-dd}-Products.csv"
        };

        return vm;
    }
}