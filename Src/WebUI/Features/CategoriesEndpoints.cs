using MediatR;
using Northwind.Application.Categories.Queries.GetCategoriesList;
using SSW.CleanArchitecture.WebApi.Extensions;

namespace Northwind.WebUI.Features;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this WebApplication app)
    {
        var group = app
            .MapApiGroup("categories");
        // TODO: Add back
        //.RequireAuthorization();

        //    [HttpGet]
        //    public async Task<ActionResult<IList<CategoryLookupDto>>> GetAll()
        //    {
        //        return Ok(await Mediator.Send(new GetCategoriesListQuery()));
        //    }
        group
            .MapGet("/", (ISender sender, CancellationToken ct) => sender.Send(new GetCategoriesListQuery(), ct))
            .WithName("GetCategoriesList")
            .ProducesGet<CategoryLookupDto[]>();
        //.RequireAuthorization();
    }
}