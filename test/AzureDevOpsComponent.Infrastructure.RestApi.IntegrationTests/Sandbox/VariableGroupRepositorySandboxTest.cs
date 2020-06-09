using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Domain.Repositories;
using AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Repositories;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Xunit;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.IntegrationTests.Repositories
{
    [Trait("Environment", "Sandbox")]
    public class VariableGroupRepositorySandboxTest : RepositoryIntegrationTestBase
    {
        public VariableGroupRepositorySandboxTest()
            : base("Sandbox")
        {
        }

        [Fact]
        public async Task VariableGroupRepositoryFindOneByIdAsync_OnSandbox_ReturnDefinition()
        {
            // Arrange
            var repository = BuildRepository();
            var variableGroupId = GetEnvironmentVariable("VariableGroup");

            // Act
            var output = await repository.FindOneByIdAsync(ProjectName, variableGroupId.ToString());

            // Assert
            output.Should().NotBeNull();
        }

        [Fact]
        public async Task VariableGroupRepositoryUpdateAsync_OnSandbox_IsSuccessful()
        {
            // Arrange
            var repository = BuildRepository();
            var variableGroupId = GetEnvironmentVariable("VariableGroup");
            var fixture = new Fixture();
            var inputCreate = fixture.CreateMany<KeyValuePair<string, string>>(5).ToDictionary(x => x.Key, x => x.Value);
            inputCreate.Add("test", DateTime.Now.ToShortDateString());
            var inputUpdate = fixture.CreateMany<KeyValuePair<string, string>>(3).ToDictionary(x => x.Key, x => x.Value);
            inputUpdate.Add("test", DateTime.Now.ToLongDateString());

            // Act & Assert
            await repository.UpdateAsync(ProjectName, variableGroupId.ToString(), inputCreate, true);
            var outputAfterFirstUpdate = await repository.FindOneByIdAsync(ProjectName, variableGroupId.ToString());
            outputAfterFirstUpdate.Should().NotBeNull();
            ((JObject)outputAfterFirstUpdate.Variables).Children().Count().Should().Be(6);

            await repository.UpdateAsync(ProjectName, variableGroupId.ToString(), inputUpdate, false);
            var outputAfterSecondUpdate = await repository.FindOneByIdAsync(ProjectName, variableGroupId.ToString());
            outputAfterSecondUpdate.Should().NotBeNull();
            ((JObject)outputAfterSecondUpdate.Variables).Children().Count().Should().Be(9);
        }

        private IVariableGroupRepository BuildRepository()
        {
            var logger = ServiceProvider.GetService<ILogger<VariableGroupRepository>>();
            var httpClientFactory = ServiceProvider.GetService<IHttpClientFactory>();
            var mapper = ServiceProvider.GetService<IMapper>();

            return new VariableGroupRepository(Configuration, logger, httpClientFactory, mapper);
        }
    }
}
