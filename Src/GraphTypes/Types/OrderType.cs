using GraphQL.Types;
using MediatR;
using Northwind.Application.Customers.Queries.GetCustomer;
using Northwind.Domain.Entities;

namespace GraphTypes.Types
{
    public class OrderType : ObjectGraphType<Order>
    {
        public OrderType(IMediator mediator)
        {
            Field(o => o.OrderId).Description("The record ID of the order");
            Field(o => o.Freight, nullable:true).Description("The freight cost for the order");
            Field(o => o.OrderDate, nullable:true).Description("Date the order was placed");
            Field(o => o.RequiredDate, nullable: true).Description("Date the order is required by");
            Field(o => o.ShipAddress).Description("Shipping destination for the order");
            Field(o => o.ShippedDate, nullable:true).Description("Date the order was shipped");
            Field<CustomerType>(
                "customer",
                resolve: context =>
                {
                    return mediator.Send(new GetCustomerQuery { CustomerId = context.Source.CustomerId });
                });



            // TODO: add order details ListGraphType
            // TODO: add employee type
            // TODO: add 
        }
    }
}
