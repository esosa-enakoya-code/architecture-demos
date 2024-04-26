using System.Text;
using System.Text.Json;

namespace Shared.Util;

public static class HttpClientExtensions
{
    static readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };
    static readonly string _mediaType = "application/json";

    public static async Task<T?> FetchAsync<T>(this HttpClient client, HttpMethod method, string uri = "") where T : class
    {
        var request = new HttpRequestMessage(method, uri);

        return await SendRequestAsync<T>(client, request);
    }

    public static async Task<T?> FetchAsync<T>(this HttpClient client, HttpMethod method, object body, string uri = "") where T : class 
    {
        var request = new HttpRequestMessage(method, uri)
        {
            Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, _mediaType)
        };

        return await SendRequestAsync<T>(client, request);
    }

    static async Task<T?> SendRequestAsync<T>(HttpClient client, HttpRequestMessage request) where T : class
    {
        var response = await client.SendAsync(request);

        if (!response.IsSuccessStatusCode) return null;
        return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync(), _jsonOptions);
    }
}
