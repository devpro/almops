using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Domain.Repositories;
using AwesomeAssertions;
using Xunit;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.UnitTests.Repositories;

[Trait("Category", "UnitTests")]
public class VariableGroupRepositoryTest : RepositoryTestBase
{
    private const string VariableGroupJson =
        """
        {
          "id": 8,
          "type": "Vsts",
          "name": "almops-vars",
          "description": "Settings",
          "variables": {
            "existing": { "value": "old" },
            "secret": { "value": null, "isSecret": true }
          }
        }
        """;

    [Fact]
    public async Task FindOneByIdAsync_ReadsVariables()
    {
        var repository = CreateRepository<IVariableGroupRepository>(VariableGroupJson);

        var variableGroup = await repository.FindOneByIdAsync("almops", "8");

        LastRequest!.RequestUri!.ToString().Should().Be("https://dev.azure.com/devpro/almops/_apis/distributedtask/variablegroups/8?api-version=7.1");
        variableGroup.Id.Should().Be(8);
        variableGroup.Name.Should().Be("almops-vars");
        ((JsonObject)variableGroup.Variables!).Select(x => x.Key).Should().Equal("existing", "secret");
    }

    [Fact]
    public async Task UpdateAsync_MergesVariablesIntoExistingGroup()
    {
        var repository = CreateRepository<IVariableGroupRepository>(VariableGroupJson);

        await repository.UpdateAsync("almops", "8", new Dictionary<string, string> { ["existing"] = "new", ["added"] = "value" });

        Requests.Select(x => x.Request.Method).Should().Equal(HttpMethod.Get, HttpMethod.Put);
        LastRequest!.RequestUri!.AbsolutePath.Should().Be("/devpro/almops/_apis/distributedtask/variablegroups/8");
        var body = JsonNode.Parse(LastRequestBody!)!;
        body["name"]!.GetValue<string>().Should().Be("almops-vars");
        var variables = body["variables"]!.AsObject();
        variables.Select(x => x.Key).Should().Equal("existing", "secret", "added");
        variables["existing"]!["value"]!.GetValue<string>().Should().Be("new");
        variables["secret"]!["isSecret"]!.GetValue<bool>().Should().BeTrue();
        variables["added"]!["value"]!.GetValue<string>().Should().Be("value");
    }

    [Fact]
    public async Task UpdateAsync_ReplaceAll_KeepsOnlyInput()
    {
        var repository = CreateRepository<IVariableGroupRepository>(VariableGroupJson);

        await repository.UpdateAsync("almops", "8", new Dictionary<string, string> { ["only"] = "one" }, isReplaceAll: true);

        var variables = JsonNode.Parse(LastRequestBody!)!["variables"]!.AsObject();
        variables.Select(x => x.Key).Should().Equal("only");
        variables["only"]!["value"]!.GetValue<string>().Should().Be("one");
    }
}
