using CsvHelper;
using Northwind.Application.Common.Interfaces;
using System.Globalization;

namespace Northwind.Infrastructure.Files;

public class CsvBuilder : ICsvBuilder
{
    public Task<byte[]> GetCsvBytes<T>(IEnumerable<T> records)
    {
        using var stream = new MemoryStream();
        using var streamWriter = new StreamWriter(stream);
        using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
        {
            csvWriter.Context.ConfigureMappingProvider<T>();
            csvWriter.WriteRecords(records);
        }

        return Task.FromResult(stream.ToArray());
    }
}