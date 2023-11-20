namespace Northwind.Application.Products.Queries.GetProductsFile;

public interface ICsvFileBuilder
{
    byte[] BuildProductsFile(IEnumerable<ProductRecordDto> records);
}