﻿using GraphQL.Types;
using GraphTypes.Queries;
using GraphTypes.Schemas;
using GraphTypes.Types;
using Microsoft.Extensions.DependencyInjection;

namespace GraphTypes
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddGraphTypes(this IServiceCollection services)
        {
            services.AddTransient<CustomerType>();
            services.AddTransient<CustomerQuery>();
            services.AddTransient<OrderType>();
            services.AddTransient<OrderQuery>();
            services.AddTransient<RootQuery>();

            services.AddTransient<ISchema, RootSchema>();
            return services;
        }
    }
}
