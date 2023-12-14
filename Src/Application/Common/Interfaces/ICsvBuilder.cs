namespace Northwind.Application.Common.Interfaces;

public interface ICsvBuilder
{
    Task<byte[]> GetCsvBytes<T>(IEnumerable<T> records);
}