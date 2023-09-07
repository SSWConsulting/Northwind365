using MediatR;
using Microsoft.AspNetCore.Mvc;
using Northwind.Application.Customers.Commands.CreateCustomer;
using Northwind.Application.Customers.Commands.DeleteCustomer;
using Northwind.Application.Customers.Commands.UpdateCustomer;
using Northwind.Application.Customers.Queries.GetCustomerDetail;
using Northwind.Application.Customers.Queries.GetCustomersList;
using SSW.CleanArchitecture.WebApi.Extensions;

namespace Northwind.WebUI.Controllers;

public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints(this WebApplication app)
    {
        var group = app
            .MapApiGroup("customers")
            .RequireAuthorization();

        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<ActionResult<CustomersListVm>> GetAll()
        //{
        //    return Ok(await Mediator.Send(new GetCustomersListQuery()));
        //}
        group
            .MapGet("/", (ISender sender, CancellationToken ct) => sender.Send(new GetCustomersListQuery(), ct))
            .WithName("GetCustomersList")
            .ProducesGet<CustomersListVm>();

        //[HttpGet("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<CustomerDetailVm>> Get(string id)
        //{
        //    return Ok(await Mediator.Send(new GetCustomerDetailQuery { Id = id }));
        //}
        group
            .MapGet("/{id}", (string id, ISender sender, CancellationToken ct) => sender.Send(new GetCustomerDetailQuery { Id = id }, ct))
            .WithName("GetCustomersList")
            .ProducesGet<CustomersListVm>();

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesDefaultResponseType]
        //public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
        //{
        //    await Mediator.Send(command);
        //    return NoContent();
        //}
        group
            .MapPost("/", ([FromBody] CreateCustomerCommand command, ISender sender, CancellationToken ct) => sender.Send(command, ct))
            .WithName("CreateCustomer")
            .ProducesPost();

        //[HttpPut("{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> Update([FromBody] UpdateCustomerCommand command)
        //{
        //    await Mediator.Send(command);
        //    return NoContent();
        //}
        group
            .MapPost("/", ([FromBody] UpdateCustomerCommand command, ISender sender, CancellationToken ct) => sender.Send(command, ct))
            .WithName("UpdateCustomer")
            .ProducesPut();

        //[HttpDelete("{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    await Mediator.Send(new DeleteCustomerCommand { Id = id });
        //    return NoContent();
        //}
        group
            .MapDelete("/{id}", (string id, ISender sender, CancellationToken ct) => sender.Send(new DeleteCustomerCommand { Id = id }, ct))
            .WithName("DeleteCustomer")
            .ProducesDelete();
    }
}