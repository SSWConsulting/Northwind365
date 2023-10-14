using AutoMapper;
using Northwind.Application.Common.Mappings;
using Northwind.Domain.Products;

namespace Northwind.Application.Products.Queries.GetProductsList;

public class ProductDto : IMapFrom<Product>
{
    public int ProductId { get; set; }

    public string ProductName { get; set; }

    public decimal? UnitPrice { get; set; }

    public int? SupplierId { get; set; }

    public string SupplierCompanyName { get; set; }

    public int? CategoryId { get; set; }

    public string CategoryName { get; set; }

    public bool Discontinued { get; set; }

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