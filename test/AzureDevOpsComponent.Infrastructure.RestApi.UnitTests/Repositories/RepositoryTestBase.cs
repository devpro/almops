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
/// so that a test covers the deserialization and the mapping together.
/// </summary>
public abstract class RepositoryTestBase
{
    protected AzureDevOpsRestApiConfiguration Configuration { get; } = new()
    {
        BaseUrl = "https://dev.azure.com/devpro",
        Username = "user",
        Token = "token"
    };

    protected HttpRequestMessage? LastRequest { get; private set; }

    protected T CreateRepository<T>(string responseJson) where T : notnull
    {
        var handler = new Mock<HttpMessageHandler>();
        handler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Callback<HttpRequestMessage, CancellationToken>((request, _) => LastRequest = request)
            .ReturnsAsync(() => new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(responseJson, Encoding.UTF8, "application/json") });

        var services = new ServiceCollection()
            .AddLogging()
            .AddAzureDevOpsRestApi(Configuration);
        services.AddHttpClient(Configuration.HttpClientName).ConfigurePrimaryHttpMessageHandler(() => handler.Object);

        return services.BuildServiceProvider().GetRequiredService<T>();
    }
}
