using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AlmOps.GitLabComponent.Infrastructure.RestApi.DependencyInjection;
using AlmOps.GitLabComponent.Infrastructure.RestApi.Repositories;
using AwesomeAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using Xunit;

namespace AlmOps.GitLabComponent.Infrastructure.RestApi.UnitTests.Repositories;

[Trait("Category", "UnitTests")]
public class AuditEventRepositoryTest
{
    private const string AuditEventsJson =
        """
        [
          {
            "id": 1205,
            "author_id": 42,
            "entity_id": 7,
            "entity_type": "Group",
            "event_type": "member_updated",
            "details": { "change": "access_level", "from": "Developer", "to": "Maintainer", "author_name": "Bertrand", "author_class": "User", "target_id": 3, "custom_message": "promoted" },
            "ip_address": "127.0.0.1",
            "author_name": "Bertrand",
            "entity_path": "devpro",
            "target_details": "almops",
            "target_type": "User",
            "target_id": 3,
            "created_at": "2026-10-20T08:00:00.000Z"
          }
        ]
        """;

    private HttpRequestMessage? _lastRequest;

    [Fact]
    public async Task FindAtGroupLevelAsync_ReadsSnakeCaseResponse()
    {
        var repository = CreateRepository();

        var events = await repository.FindAtGroupLevelAsync("7");

        _lastRequest!.RequestUri!.ToString().Should().Be("https://gitlab.com/api/v4/groups/7/audit_events");
        _lastRequest.Headers.Authorization!.Scheme.Should().Be("Bearer");
        events.Should().ContainSingle();
        var auditEvent = events[0];
        auditEvent.Id.Should().Be(1205);
        auditEvent.AuthorId.Should().Be(42);
        auditEvent.EntityType.Should().Be("Group");
        auditEvent.EventType.Should().Be("member_updated");
        auditEvent.EntityPath.Should().Be("devpro");
        auditEvent.TargetId.Should().Be(3);
        auditEvent.Details.Change.Should().Be("access_level");
        auditEvent.Details.To.Should().Be("Maintainer");
        auditEvent.Details.AuthorClass.Should().Be("User");
        auditEvent.Details.CustomMessage.Should().Be("promoted");
    }

    [Fact]
    public async Task FindAtProjectLevelAsync_CallsProjectEndpoint()
    {
        var repository = CreateRepository();

        var events = await repository.FindAtProjectLevelAsync("12");

        _lastRequest!.RequestUri!.ToString().Should().Be("https://gitlab.com/api/v4/projects/12/audit_events");
        events.Should().ContainSingle();
    }

    [Fact]
    public async Task FindAtInstanceLevelAsync_CallsInstanceEndpoint()
    {
        var repository = CreateRepository();

        await repository.FindAtInstanceLevelAsync();

        _lastRequest!.RequestUri!.ToString().Should().Be("https://gitlab.com/api/v4/audit_events");
    }

    private AuditEventRepository CreateRepository()
    {
        var handler = new Mock<HttpMessageHandler>();
        handler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Callback<HttpRequestMessage, CancellationToken>((request, _) => _lastRequest = request)
            .ReturnsAsync(() => new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(AuditEventsJson, Encoding.UTF8, "application/json") });

        var services = new ServiceCollection()
            .AddLogging()
            .AddGitLabRestApi(new GitLabRestApiConfiguration { Token = "glpat-token" });
        services.AddHttpClient(GitLabRestApiConfiguration.HttpClientName).ConfigurePrimaryHttpMessageHandler(() => handler.Object);

        return services.BuildServiceProvider().GetRequiredService<AuditEventRepository>();
    }
}
