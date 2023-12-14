using CsvHelper.Configuration;
using Northwind.Application.Customers.Queries.GetCustomersCsv;
using System.Globalization;

namespace Northwind.Infrastructure.Files;

public sealed class CustomerFileRecordMap : ClassMap<CustomerCsvLookupDto>
{
    public CustomerFileRecordMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
    }
}