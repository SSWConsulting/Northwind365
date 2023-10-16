using AutoMapper;
using Northwind.Application.Common.Mappings;
using Northwind.Domain.Products;

namespace Northwind.Application.Products.Queries.GetProductsList;

public class ProductDto : IMapFrom<Product>
{
    public int ProductId { get; init; }

    public required string ProductName { get; init; }

    public decimal? UnitPrice { get; init; }

    public int? SupplierId { get; init; }

    public required string SupplierCompanyName { get; init; }

    public int? CategoryId { get; init; }

    public required string CategoryName { get; init; }

    public bool Discontinued { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ProductDto>()
            .ForMember(d => d.ProductId, opt => opt.MapFrom(s => s.Id.Value))
            .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => s.SupplierId.GetValueOrDefault().Value))
            .ForMember(d => d.CategoryId, opt => opt.MapFrom(s => s.CategoryId.GetValueOrDefault().Value))
            .ForMember(d => d.SupplierCompanyName, opt => opt.MapFrom(s => s.Supplier != null ? s.Supplier.CompanyName : string.Empty))
            .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category != null ? s.Category.CategoryName : string.Empty));
    }
}