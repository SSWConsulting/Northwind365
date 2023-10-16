using Newtonsoft.Json;
using System.Text;

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
        if (result is null)
            throw new NullReferenceException("Not able to deserialize response");

        return result;
    }
}