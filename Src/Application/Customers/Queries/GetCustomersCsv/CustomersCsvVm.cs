namespace Northwind.Application.Customers.Queries.GetCustomersCsv;

public class CustomersCsvVm
{
    public required byte[] Data { get; set; }
    public required string FileName { get; set; }
    public readonly string ContentType = "text/csv";
}