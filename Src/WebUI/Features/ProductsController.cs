using MediatR;
using Northwind.Application.Products.Commands.CreateProduct;
using Northwind.Application.Products.Commands.DeleteProduct;
using Northwind.Application.Products.Commands.UpdateProduct;
using Northwind.Application.Products.Queries.GetProductDetail;
using Northwind.Application.Products.Queries.GetProductsFile;
using Northwind.Application.Products.Queries.GetProductsList;
using SSW.CleanArchitecture.WebApi.Extensions;

namespace Northwind.WebUI.Controllers;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        var group = app
            .MapApiGroup("products")
            .RequireAuthorization();

        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<ActionResult<ProductsListVm>> GetAll()
        //{
        //    return Ok(await Mediator.Send(new GetProductsListQuery()));
        //}
        group
            .MapGet("/", (ISender sender, CancellationToken ct) => sender.Send(new GetProductsListQuery(), ct))
            .WithName("GetProductsList")
            .ProducesGet<ProductsListVm>()
            .AllowAnonymous();

        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<FileResult> Download()
        //{
        //    var vm = await Mediator.Send(new GetProductsFileQuery());
        //    return File(vm.Content, vm.ContentType, vm.FileName);
        //}
        group
            .MapGet("/", async (ISender sender, CancellationToken ct) =>
            {
                var file = await sender.Send(new GetProductsFileQuery(), ct);
                return TypedResults.File(file.Content, file.ContentType, file.FileName);
            })
            .WithName("Download")
            .ProducesGet<ProductsListVm>()
            .AllowAnonymous();

        //[HttpGet("{id}")]
        //public async Task<ActionResult<ProductDetailVm>> Get(int id)
        //{
        //    return Ok(await Mediator.Send(new GetProductDetailQuery { Id = id }));
        //}
        group
            .MapGet("/{id}", (int id, ISender sender, CancellationToken ct) => sender.Send(new GetProductDetailQuery { Id = id }, ct))
            .WithName("GetProductDetail")
            .ProducesGet<ProductDetailVm>();

        //[HttpPost]
        //public async Task<ActionResult<int>> Create([FromBody] CreateProductCommand command)
        //{
        //    var productId = await Mediator.Send(command);
        //    return Ok(productId);
        //}
        group
            .MapPost("/", (CreateProductCommand command, ISender sender, CancellationToken ct) => sender.Send(command, ct))
            .WithName("CreateProduct")
            .ProducesPost<int>();

        //[HttpPut]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesDefaultResponseType]
        //public async Task<IActionResult> Update([FromBody] UpdateProductCommand command)
        //{
        //    await Mediator.Send(command);
        //    return NoContent();
        //}
        group
            .MapPut("/", (UpdateProductCommand command, ISender sender, CancellationToken ct) => sender.Send(command, ct))
            .WithName("UpdateProduct")
            .ProducesPut();

        //[HttpDelete("{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesDefaultResponseType]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    await Mediator.Send(new DeleteProductCommand { Id = id });
        //    return NoContent();
        //}
        group
            .MapPut("/{id}", (int id, ISender sender, CancellationToken ct) => sender.Send(new DeleteProductCommand { Id = id }, ct))
            .WithName("DeleteProduct")
            .ProducesDelete();
    }
}