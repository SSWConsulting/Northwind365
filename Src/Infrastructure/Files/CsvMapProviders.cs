using CsvHelper;
using Northwind.Application.Customers.Queries.GetCustomersCsv;
using Northwind.Application.Products.Queries.GetProductsFile;

namespace Northwind.Infrastructure.Files;

public static class CsvMapProviders
{
    private static readonly IReadOnlyDictionary<Type, Action<CsvContext>> TypeConfiguration = new Dictionary<Type, Action<CsvContext>>
    {
        { typeof(ProductRecordDto), context => context.RegisterClassMap<ProductFileRecordMap>() },
        { typeof(CustomerCsvLookupDto), context => context.RegisterClassMap<CustomerFileRecordMap>() },
    };
    
    public static void ConfigureMappingProvider<T>(
        this CsvContext csvContext)
    {
        TypeConfiguration.GetValueOrDefault(typeof(T))?.Invoke(csvContext);
    }
}