using GraphQL.Types;

namespace GraphTypes.Queries
{
    public class RootQuery : ObjectGraphType
    {
        public RootQuery()
        {
            Field<CustomerQuery>("customersQuery", resolve: context => new { });
        }
    }
}
