using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AlmOps.Common.Infrastructure.RestApi.Json;
using Microsoft.Extensions.Logging;

namespace AlmOps.Common.Infrastructure.RestApi;

/// <summary>
/// Base of the repositories calling a REST API with JSON content, through a named HTTP client.
/// </summary>
public abstract class HttpRepositoryBase(ILogger logger, IHttpClientFactory httpClientFactory)
{
    protected ILogger Logger { get; } = logger;

    protected abstract string HttpClientName { get; }

    /// <summary>
    /// Gets the resource at the URL, which must answer with a success and a body.
    /// </summary>
    /// <exception cref="ConnectivityException"></exception>
    protected async Task<T> GetAsync<T>(string url)
    {
        var response = await CreateHttpClient().GetAsync(url);
        return await ReadResultAsync<T>(url, "GET", response);
    }

    /// <summary>
    /// Posts the body to the URL, which must answer with a success and a body.
    /// </summary>
    /// <exception cref="ConnectivityException"></exception>
    protected async Task<T> PostAsync<T>(string url, object body)
    {
        var response = await CreateHttpClient().PostAsync(url, CreateContent(body));
        return await ReadResultAsync<T>(url, "POST", response);
    }

    /// <summary>
    /// Puts the body to the URL, which must answer with a success.
    /// </summary>
    /// <exception cref="ConnectivityException"></exception>
    protected async Task PutAsync(string url, object body)
    {
        var response = await CreateHttpClient().PutAsync(url, CreateContent(body));
        CheckStatusCode(url, response, await response.Content.ReadAsStringAsync());
    }

    private HttpClient CreateHttpClient()
    {
        return httpClientFactory.CreateClient(HttpClientName);
    }

    private static StringContent CreateContent(object body)
    {
        return new StringContent(JsonSerializer.Serialize(body, JsonDefaults.Options), Encoding.UTF8, "application/json");
    }

    private async Task<T> ReadResultAsync<T>(string url, string httpMethodName, HttpResponseMessage response)
    {
        var result = await response.Content.ReadAsStringAsync();
        CheckStatusCode(url, response, result);
        if (string.IsNullOrEmpty(result))
        {
            throw new ConnectivityException($"Empty response received while calling {url}");
        }

        try
        {
            return JsonSerializer.Deserialize<T>(result, JsonDefaults.Options)
                   ?? throw new ConnectivityException($"Null response received while calling {url}");
        }
        catch (JsonException exc)
        {
            Logger.LogWarning("Cannot deserialize {HttpMethodName} call response content [HttpRequestUrl={Url}] [SerializationType={Type}] [ExceptionMessage={ExceptionMessage}]",
                httpMethodName, url, typeof(T), exc.Message);
            Logger.LogDebug("[HttpResponseContent={Result}]", result);
            throw new ConnectivityException($"Invalid data received when calling \"{url}\": {exc.Message}", exc);
        }
    }

    private void CheckStatusCode(string url, HttpResponseMessage response, string result)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        Logger.LogDebug("Status code doesn't indicate success [HttpRequestUrl={Url}] [HttpResponseStatusCode={ResponseStatusCode}] [HttpResponseContent={Result}]",
            url, response.StatusCode, result);
        throw new ConnectivityException($"The response status \"{response.StatusCode}\" is not a success (reason=\"{response.ReasonPhrase}\")");
    }
}
