using GraphQL;
using GraphQL.Types;
using GraphTypes.Types;
using MediatR;
using Northwind.Application.Customers.Commands.CreateGraphCustomer;
using Northwind.Application.Customers.Commands.DeleteCustomer;
using Northwind.Application.Customers.Commands.UpdateGraphCustomer;

namespace GraphTypes.Mutations
{
    public class CustomerMutation : ObjectGraphType
    {
        public CustomerMutation(IMediator mediator)
        {
            Field<CustomerType>(
                "createCustomer",
                arguments: new QueryArguments(
                    new QueryArgument<CustomerInputType> { Name = "customer" }),
                resolve: context =>
                {
                    var createCustomerCommand = context.GetArgument<CreateGraphCustomerCommand>("customer");
                    return mediator.Send(createCustomerCommand);
                });


            Field<CustomerType>(
                "updateCustomer",
                arguments: new QueryArguments(
                    new QueryArgument<CustomerInputType> { Name = "customer" }),
                resolve: context =>
                {
                    var updateCustomerCommand = context.GetArgument<UpdateGraphCustomerCommand>("customer");
                    return mediator.Send(updateCustomerCommand);
                });

            Field<StringGraphType>(
                "deleteCustomer",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = "customerId" }),
                resolve: context =>
                {
                    mediator.Send(new DeleteCustomerCommand { Id = context.GetArgument<string>("customerId") });
                    return "Customer deleted";
                });

        }
    }
}
