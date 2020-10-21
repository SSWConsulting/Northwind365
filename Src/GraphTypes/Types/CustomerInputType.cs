using GraphQL.Types;

namespace GraphTypes.Types
{
    public class CustomerInputType : InputObjectGraphType
    {
        public CustomerInputType()
        {
            Field<StringGraphType>("Id", "Customer record ID");
            Field<StringGraphType>("Address", "Company Street Address");
            Field<StringGraphType>("City", "City where customer is located");
            Field<StringGraphType>("CompanyName", "Company name of customer");
            Field<StringGraphType>("ContactName", "Name of primary contact");
            Field<StringGraphType>("ContactTitle", "Primary contact's title");
            Field<StringGraphType>("Country", "Country where company is located");
            Field<StringGraphType>("Fax", "[Obsolete] Main fax number of company");
            Field<StringGraphType>("Phone", "Main phone number of company");
            Field<StringGraphType>("PostalCode", "Company post code");
        }
    }
}
