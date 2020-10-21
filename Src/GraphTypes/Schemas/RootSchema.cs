﻿using GraphQL.Utilities;
using GraphTypes.Queries;
using System;

namespace GraphTypes.Schemas
{
    public class RootSchema : GraphQL.Types.Schema
    {
        public RootSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<RootQuery>();
        }
    }
}
