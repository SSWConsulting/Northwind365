using MediatR;
using Microsoft.AspNetCore.Mvc;
using Northwind.Application.Products.Commands.CreateProduct;
using Northwind.Application.Products.Commands.DeleteProduct;
using Northwind.Application.Products.Commands.UpdateProduct;
using Northwind.Application.Products.Queries.GetProductDetail;
using Northwind.Application.Products.Queries.GetProductsFile;
using Northwind.Application.Products.Queries.GetProductsList;
using SSW.CleanArchitecture.WebApi.Extensions;

namespace Northwind.WebUI.Features;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        var group = app
            .MapApiGroup("products")
            .AllowAnonymous();

        group
            .MapGet("/", (ISender sender, CancellationToken ct) => sender.Send(new GetProductsListQuery(), ct))
            .WithName("GetProductsList")
            .ProducesGet<ProductsListVm>();

        group
            .MapGet("/download", async (ISender sender, CancellationToken ct) =>
            {
                var file = await sender.Send(new GetProductsFileQuery(), ct);
                return TypedResults.File(file.Content, file.ContentType, file.FileName);
            })
            .WithName("GetProductsCsv")
            .ProducesGet<FileStreamResult>();

        group
            .MapGet("/{id}",
                (int id, ISender sender, CancellationToken ct) => sender.Send(new GetProductDetailQuery(id), ct))
            .WithName("GetProductDetail")
            .ProducesGet<ProductDetailVm>();

        group
            .MapPost("/",
                (CreateProductCommand command, ISender sender, CancellationToken ct) => sender.Send(command, ct))
            .WithName("CreateProduct")
            .ProducesPost<int>();

        group
            .MapPut("/",
                (UpdateProductCommand command, ISender sender, CancellationToken ct) => sender.Send(command, ct))
            .WithName("UpdateProduct")
            .ProducesPut();

        group
            .MapDelete("/{id}",
                (int id, ISender sender, CancellationToken ct) => sender.Send(new DeleteProductCommand(id), ct))
            .WithName("DeleteProduct")
            .ProducesDelete();
    }
}