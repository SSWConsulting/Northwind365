using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

using Northwind.Domain.Categories;
using Northwind.Domain.Customers;
using Northwind.Domain.Products;
using Northwind.Domain.Supplying;
using Northwind.Persistence;

namespace Northwind.WebUI.IntegrationTests.Common;

public static class Utilities
{
    public static StringContent GetRequestContent(object obj)
    {
        return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
    }

    public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
    {
        var stringResponse = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<T>(stringResponse);

        return result;
    }
}