using MediatR;
using Microsoft.AspNetCore.Mvc;
using Northwind.Application.Customers.Commands.CreateCustomer;
using Northwind.Application.Customers.Commands.DeleteCustomer;
using Northwind.Application.Customers.Commands.UpdateCustomer;
using Northwind.Application.Customers.Queries.GetCustomerDetail;
using Northwind.Application.Customers.Queries.GetCustomersCsv;
using Northwind.Application.Customers.Queries.GetCustomersList;
using SSW.CleanArchitecture.WebApi.Extensions;

namespace Northwind.WebUI.Features;

public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints(this WebApplication app)
    {
        var group = app
            .MapApiGroup("customers")
            .RequireAuthorization();

        group
            .MapGet("/", (ISender sender, CancellationToken ct) => sender.Send(new GetCustomersListQuery(), ct))
            .WithName("GetCustomersList")
            .ProducesGet<CustomersListVm>();

        group
            .MapGet("/download", async (ISender sender, CancellationToken ct) =>
            {
                CustomersCsvVm result = await sender.Send(new GetCustomersCsvQuery(), ct);
                return TypedResults.File(result.Data, result.ContentType, result.FileName);
            })
            .WithName("GetCustomersCsv")
            .ProducesGet<FileStreamResult>();

        group
            .MapGet("/{id}",
                (string id, ISender sender, CancellationToken ct) => sender.Send(new GetCustomerDetailQuery(id), ct))
            .WithName("GetCustomer")
            .ProducesGet<CustomerDetailVm>();

        group
            .MapPost("/",
                ([FromBody] CreateCustomerCommand command, ISender sender, CancellationToken ct) =>
                    sender.Send(command, ct))
            .WithName("CreateCustomer")
            .ProducesPost();

        group
            .MapPut("/{id}",
                (string id, [FromBody] UpdateCustomerCommand command, ISender sender, CancellationToken ct) =>
                    sender.Send(command with { Id = id }, ct))
            .WithName("UpdateCustomer")
            .ProducesPut();

        group
            .MapDelete("/{id}",
                (string id, ISender sender, CancellationToken ct) => sender.Send(new DeleteCustomerCommand(id), ct))
            .WithName("DeleteCustomer")
            .ProducesDelete();
    }
}