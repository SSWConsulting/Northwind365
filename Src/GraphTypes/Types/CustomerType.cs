﻿using GraphQL.Types;
using MediatR;
using Northwind.Application.Orders.Queries.GetCustomerOrders;
using Northwind.Domain.Entities;

namespace GraphTypes.Types
{
    public class CustomerType : ObjectGraphType<Customer>
    {
        public CustomerType(IMediator mediator)
        {
            Field(c => c.CustomerId).Description("The record ID of the customer");
            Field(c => c.CompanyName).Description("The name of the comany");
            Field(c => c.ContactName).Description("Name of the primary contact at the company");
            Field(c => c.ContactTitle).Description("Title of the primary contact at the company");
            Field(c => c.Country).Description("Country in which the company is located");
            Field(c => c.Address).Description("Company's street address");
            Field(c => c.City).Description("City in which the company is located");
            Field(c => c.Fax).Description("[Obsolete] Main fax number for the company");
            Field(c => c.Phone).Description("Main phone number for the company");
            Field(c => c.PostalCode).Description("Post code for the company's address");
            Field(c => c.Region).Description("Company region");

            Field<ListGraphType<OrderType>>(
                "orders",
                resolve: context =>
                {
                    return mediator.Send(new GetCustomerOrdersQuery { CustomerId = context.Source.CustomerId });
                });
        }
    }
}