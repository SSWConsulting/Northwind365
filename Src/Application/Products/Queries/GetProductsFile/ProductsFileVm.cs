namespace Northwind.Application.Products.Queries.GetProductsFile;

public class ProductsFileVm
{
    public required string FileName { get; init; }
    public required string ContentType { get; init; }
    public required byte[] Content { get; init; }
}