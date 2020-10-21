using GraphQL;
using GraphQL.Types;
using GraphTypes.Types;
using MediatR;
using Northwind.Application.Orders.Queries.GetCustomerOrders;
using Northwind.Application.Orders.Queries.GetOrder;
using Northwind.Application.Orders.Queries.GetOrders;

namespace GraphTypes.Queries
{
    public class OrderQuery : ObjectGraphType
    {
        public OrderQuery(IMediator mediator)
        {
            Field<ListGraphType<OrderType>>(
                "orders",
                resolve: context =>
                {
                    return mediator.Send(new GetOrdersQuery());
                });

            Field<OrderType>(
                "orderById",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "orderid" }),
                resolve: context =>
                {
                    return mediator.Send(new GetOrderQuery { OrderId = context.GetArgument<int>("orderid") });
                });

            Field<ListGraphType<OrderType>>(
                "customerOrders",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "customerId" }),
                resolve: context =>
                {
                    return mediator.Send(new GetCustomerOrdersQuery { CustomerId = context.GetArgument<string>("customerId") });
                });
        }
    }
}
