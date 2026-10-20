using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Domain.Repositories;
using AwesomeAssertions;
using Xunit;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.UnitTests.Repositories;

[Trait("Category", "UnitTests")]
public class ProjectRepositoryTest : RepositoryTestBase
{
    [Fact]
    public async Task FindAllAsync_ReadsCamelCaseResponse()
    {
        var repository = CreateRepository<IProjectRepository>(
            """
            {
              "count": 2,
              "value": [
                { "id": "eb6e4656", "name": "almops", "description": "ALM tool", "url": "https://dev.azure.com/devpro/_apis/projects/eb6e4656", "state": "wellFormed", "revision": 411, "visibility": "public", "lastUpdateTime": "2026-10-20T08:00:00Z" },
                { "id": "6ce954b1", "name": "keeptrack", "state": "wellFormed", "revision": 12, "visibility": "private" }
              ]
            }
            """);

        var projects = await repository.FindAllAsync();

        LastRequest!.RequestUri!.ToString().Should().Be("https://dev.azure.com/devpro/_apis/projects?api-version=7.1");
        LastRequest.Headers.Authorization!.Scheme.Should().Be("Basic");
        projects.Should().HaveCount(2);
        projects[0].Id.Should().Be("eb6e4656");
        projects[0].Name.Should().Be("almops");
        projects[0].Description.Should().Be("ALM tool");
        projects[0].State.Should().Be("wellFormed");
        projects[0].Revision.Should().Be(411);
        projects[0].Visibility.Should().Be("public");
        projects[1].Name.Should().Be("keeptrack");
    }
}
