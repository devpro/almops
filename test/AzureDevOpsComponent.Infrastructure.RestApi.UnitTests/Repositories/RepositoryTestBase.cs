using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.UnitTests.Repositories;

/// <summary>
/// Resolves repositories from the registration the application uses, with an HTTP handler answering the JSON the API sends,
/// so that a test covers the serialization, the deserialization and the mapping together.
/// </summary>
public abstract class RepositoryTestBase
{
    protected AzureDevOpsRestApiConfiguration Configuration { get; } = new()
    {
        BaseUrl = "https://dev.azure.com/devpro",
        Username = "user",
        Token = "token"
    };

    protected List<(HttpRequestMessage Request, string? Body)> Requests { get; } = [];

    protected HttpRequestMessage? LastRequest => Requests.Count == 0 ? null : Requests[^1].Request;

    protected string? LastRequestBody => Requests.Count == 0 ? null : Requests[^1].Body;

    protected T CreateRepository<T>(string responseJson, HttpStatusCode statusCode = HttpStatusCode.OK) where T : notnull
    {
        var handler = new Mock<HttpMessageHandler>();
        handler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            // The body is read when the request is sent, as the client may dispose of its content afterwards.
            .Callback<HttpRequestMessage, CancellationToken>((request, _) => Requests.Add((request, request.Content?.ReadAsStringAsync().GetAwaiter().GetResult())))
            .ReturnsAsync(() => new HttpResponseMessage(statusCode) { Content = new StringContent(responseJson, Encoding.UTF8, "application/json") });

        var services = new ServiceCollection()
            .AddLogging()
            .AddAzureDevOpsRestApi(Configuration);
        services.AddHttpClient(Configuration.HttpClientName).ConfigurePrimaryHttpMessageHandler(() => handler.Object);

        return services.BuildServiceProvider().GetRequiredService<T>();
    }
}
