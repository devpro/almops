using System;
using System.Linq;
using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Domain.Repositories;
using AwesomeAssertions;
using Bogus;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Xunit;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.IntegrationTests.Sandbox;

[Trait("Environment", "Sandbox")]
public class VariableGroupRepositorySandboxTest() : RepositoryIntegrationTestBase("Sandbox")
{
    [Fact]
    public async Task VariableGroupRepositoryFindOneByIdAsync_OnSandbox_ReturnDefinition()
    {
        // Arrange
        var repository = BuildRepository();
        var variableGroupId = GetEnvironmentVariable("VariableGroupId");

        // Act
        var output = await repository.FindOneByIdAsync(ProjectName, variableGroupId);

        // Assert
        output.Should().NotBeNull();
    }

    [Fact]
    public async Task VariableGroupRepositoryUpdateAsync_OnSandbox_IsSuccessful()
    {
        // Arrange
        var repository = BuildRepository();
        var variableGroupId = GetEnvironmentVariable("VariableGroupId");
        var faker = new Faker();
        var inputCreate = Enumerable.Range(0, 5).ToDictionary(_ => faker.Random.AlphaNumeric(12), _ => faker.Lorem.Word());
        inputCreate.Add("test", DateTime.Now.ToShortDateString());
        var inputUpdate = Enumerable.Range(0, 3).ToDictionary(_ => faker.Random.AlphaNumeric(12), _ => faker.Lorem.Word());
        inputUpdate.Add("test", DateTime.Now.ToLongDateString());

        // Act & Assert
        await repository.UpdateAsync(ProjectName, variableGroupId, inputCreate, true);
        var outputAfterFirstUpdate = await repository.FindOneByIdAsync(ProjectName, variableGroupId);
        outputAfterFirstUpdate.Should().NotBeNull();
        ((JObject)outputAfterFirstUpdate.Variables!).Children().Count().Should().Be(6);

        await repository.UpdateAsync(ProjectName, variableGroupId, inputUpdate);
        var outputAfterSecondUpdate = await repository.FindOneByIdAsync(ProjectName, variableGroupId);
        outputAfterSecondUpdate.Should().NotBeNull();
        ((JObject)outputAfterSecondUpdate.Variables!).Children().Count().Should().Be(9);
    }

    private IVariableGroupRepository BuildRepository()
    {
        return ServiceProvider.GetRequiredService<IVariableGroupRepository>();
    }
}
