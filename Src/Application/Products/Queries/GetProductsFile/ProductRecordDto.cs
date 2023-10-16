using AutoMapper;
using Northwind.Application.Common.Mappings;
using Northwind.Domain.Products;

namespace Northwind.Application.Products.Queries.GetProductsFile;

public class ProductRecordDto : IMapFrom<Product>
{
    public required string Category { get; init; }

    public required string Name { get; init; }

    public decimal? UnitPrice { get; init; }

    public bool Discontinued { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ProductRecordDto>()
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.ProductName))
            .ForMember(d => d.Category, opt => opt.MapFrom(s => s.Category != null ? s.Category.CategoryName : string.Empty));
    }
}