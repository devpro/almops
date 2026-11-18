using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Domain.Repositories;
using AlmOps.Common.Infrastructure.RestApi;
using AwesomeAssertions;
using Xunit;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.UnitTests.Repositories;

[Trait("Category", "UnitTests")]
public class BuildRepositoryTest : RepositoryTestBase
{
    private const string BuildJson =
        """
        {
          "id": 264,
          "buildNumber": "20261020.1",
          "status": "notStarted",
          "definition": { "id": 3, "name": "almops-ci" },
          "project": { "id": "eb6e4656", "name": "almops" },
          "sourceBranch": "refs/heads/main",
          "_links": { "web": { "href": "https://dev.azure.com/devpro/almops/_build/results?buildId=264" } }
        }
        """;

    [Fact]
    public async Task FindOneByIdAsync_ReadsNumericIdAsText()
    {
        var repository = CreateRepository<IBuildRepository>(BuildJson);

        var build = await repository.FindOneByIdAsync("almops", "264");

        LastRequest!.RequestUri!.ToString().Should().Be("https://dev.azure.com/devpro/almops/_apis/build/builds/264?api-version=7.1");
        build.Id.Should().Be("264");
        build.BuildNumber.Should().Be("20261020.1");
        build.Definition!.Id.Should().Be("3");
        build.Project!.Name.Should().Be("almops");
    }

    [Fact]
    public async Task FindAllAsync_FiltersOnBranchAndDefinition()
    {
        var repository = CreateRepository<IBuildRepository>($$"""{ "count": 1, "value": [ {{BuildJson}} ] }""");

        var builds = await repository.FindAllAsync("almops", "main", "3");

        LastRequest!.RequestUri!.Query.Should().Be("?api-version=7.1&queryOrder=startTimeDescending&$top=10&branchName=refs/heads/main&definitions=3");
        builds.Should().ContainSingle().Which.Id.Should().Be("264");
    }

    [Fact]
    public async Task CreateAsync_PostsDefinitionBranchAndParameters()
    {
        var repository = CreateRepository<IBuildRepository>(BuildJson);

        var build = await repository.CreateAsync("almops", "3", "main", new Dictionary<string, string> { ["env"] = "dev" });

        LastRequest!.Method.Should().Be(HttpMethod.Post);
        LastRequest.RequestUri!.ToString().Should().Be("https://dev.azure.com/devpro/almops/_apis/build/builds?api-version=7.1");
        var body = JsonNode.Parse(LastRequestBody!)!;
        body["definition"]!["id"]!.GetValue<string>().Should().Be("3");
        body["sourceBranch"]!.GetValue<string>().Should().Be("main");
        body["parameters"]!.GetValue<string>().Should().Be("{\"env\":\"dev\"}");
        build.Id.Should().Be("264");
    }

    [Fact]
    public async Task FindOneByIdAsync_OnErrorStatus_Throws()
    {
        var repository = CreateRepository<IBuildRepository>("""{ "message": "TF401019" }""", HttpStatusCode.NotFound);

        var act = () => repository.FindOneByIdAsync("almops", "404");

        (await act.Should().ThrowAsync<ConnectivityException>()).Which.Message.Should().Be("The response status \"NotFound\" is not a success (reason=\"Not Found\")");
    }

    [Fact]
    public async Task FindOneByIdAsync_OnEmptyResponse_Throws()
    {
        var repository = CreateRepository<IBuildRepository>("");

        var act = () => repository.FindOneByIdAsync("almops", "264");

        (await act.Should().ThrowAsync<ConnectivityException>()).Which.Message.Should().StartWith("Empty response received while calling https://dev.azure.com/devpro/almops/_apis/build/builds/264");
    }
}
