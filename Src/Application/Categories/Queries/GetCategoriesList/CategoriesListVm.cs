namespace Northwind.Application.Categories.Queries.GetCategoriesList;

public class CategoriesListVm
{
    public required IList<CategoryLookupDto> Categories { get; init; }
}