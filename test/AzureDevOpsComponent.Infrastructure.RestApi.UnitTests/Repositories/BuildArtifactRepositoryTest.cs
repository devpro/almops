using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Domain.Repositories;
using AwesomeAssertions;
using Xunit;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.UnitTests.Repositories;

[Trait("Category", "UnitTests")]
public class BuildArtifactRepositoryTest : RepositoryTestBase
{
    [Fact]
    public async Task FindAllAsync_ReadsNestedResource()
    {
        var repository = CreateRepository<IBuildArtifactRepository>(
            """
            {
              "count": 1,
              "value": [
                {
                  "id": 90,
                  "name": "drop",
                  "source": "11",
                  "resource": {
                    "type": "Container",
                    "data": "#/1/drop",
                    "properties": { "localpath": "/home/vsts/work/1/a", "artifactsize": "2048" },
                    "url": "https://dev.azure.com/devpro/almops/_apis/build/builds/90/artifacts?artifactName=drop",
                    "downloadUrl": "https://dev.azure.com/devpro/almops/_apis/build/builds/90/artifacts?artifactName=drop&$format=zip"
                  }
                }
              ]
            }
            """);

        var artifacts = await repository.FindAllAsync("almops", "90");

        LastRequest!.RequestUri!.AbsolutePath.Should().Be("/devpro/almops/_apis/build/builds/90/artifacts");
        artifacts.Should().ContainSingle();
        var artifact = artifacts[0];
        artifact.Id.Should().Be(90);
        artifact.Name.Should().Be("drop");
        artifact.ResourceType.Should().Be("Container");
        artifact.ResourceLocalPath.Should().Be("/home/vsts/work/1/a");
        artifact.ResourceArtifactSize.Should().Be("2048");
        artifact.ResourceDownloadUrl.Should().EndWith("&$format=zip");
    }
}
