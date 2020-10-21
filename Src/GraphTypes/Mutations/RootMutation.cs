using GraphQL.Types;

namespace GraphTypes.Mutations
{
    public class RootMutation : ObjectGraphType
    {
        public RootMutation()
        {
            Field<CustomerMutation>("customerMutation", resolve: context => new { });
        }
    }
}
