using GraphQL.Types;

namespace GraphTypes.Queries
{
    public class RootQuery : ObjectGraphType
    {
        public RootQuery()
        {
            Field<CustomerQuery>("customersQuery", resolve: context => new { });
            Field<OrderQuery>("orderQuery", resolve: context => new { });
        }
    }
}
