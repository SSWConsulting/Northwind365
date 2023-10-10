using AutoMapper;
using Northwind.Application.Categories.Queries.GetCategoriesList;
using Northwind.Application.Customers.Queries.GetCustomerDetail;
using Northwind.Application.Customers.Queries.GetCustomersList;
using Northwind.Application.Products.Queries.GetProductsList;
using Northwind.Application.Products.Queries.GetProductDetail;
using Northwind.Application.Products.Queries.GetProductsFile;
using Northwind.Application.UnitTests.Common;
using Northwind.Domain.Categories;
using Northwind.Domain.Customers;
using Northwind.Domain.Products;

using Shouldly;
using Xunit;

namespace Northwind.Application.UnitTests;

public class MappingTests : IClassFixture<MappingTestsFixture>
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests(MappingTestsFixture fixture)
    {
        _configuration = fixture.ConfigurationProvider;
        _mapper = fixture.Mapper;
    }

    [Fact]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    [Fact]
    public void ShouldMapCategoryToCategoryLookupDto()
    {
        var entity = new Category("category", "description", new byte[] { 0x20, 0x20, 0x20 });

        var result = _mapper.Map<CategoryLookupDto>(entity);

        result.ShouldNotBeNull();
        result.ShouldBeOfType<CategoryLookupDto>();
    }

    [Fact]
    public void ShouldMapCustomerToCustomerLookupDto()
    {
        var entity = CustomerFactory.Generate();

        var result = _mapper.Map<CustomerLookupDto>(entity);

        result.ShouldNotBeNull();
        result.ShouldBeOfType<CustomerLookupDto>();
    }

    [Fact]
    public void ShouldMapProductToProductDetailVm()
    {
        var entity = ProductFactory.Generate();

        var result = _mapper.Map<ProductDetailVm>(entity);

        result.ShouldNotBeNull();
        result.ShouldBeOfType<ProductDetailVm>();
    }

    [Fact]
    public void ShouldMapProductToProductDto()
    {
        var entity = ProductFactory.Generate();

        var result = _mapper.Map<ProductDto>(entity);

        result.ShouldNotBeNull();
        result.ShouldBeOfType<ProductDto>();
    }

    [Fact]
    public void ShouldMapProductToProductRecordDto()
    {
        var entity = ProductFactory.Generate();

        var result = _mapper.Map<ProductRecordDto>(entity);

        result.ShouldNotBeNull();
        result.ShouldBeOfType<ProductRecordDto>();
    }

    [Fact]
    public void ShouldMapCustomerToCustomerDetailVm()
    {
        var entity = CustomerFactory.Generate();

        var result = _mapper.Map<CustomerDetailVm>(entity);

        result.ShouldNotBeNull();
        result.ShouldBeOfType<CustomerDetailVm>();
    }
}