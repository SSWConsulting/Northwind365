using AutoMapper;
using Northwind.Application.Common.Mappings;
using Northwind.Domain.Categories;

namespace Northwind.Application.Categories.Queries.GetCategoriesList;

public class CategoryLookupDto : IMapFrom<Category>
{
    public required int Id { get; init; }
    public required string Name { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Category, CategoryLookupDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.Value))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.CategoryName));
    }
}