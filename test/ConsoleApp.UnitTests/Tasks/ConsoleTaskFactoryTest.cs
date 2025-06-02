using AlmOps.AzureDevOpsComponent.Infrastructure.RestApi;
using AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.DependencyInjection;
using AlmOps.ConsoleApp.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AlmOps.ConsoleApp.UnitTests.Tasks
{
    [Trait("Category", "UnitTests")]
    public class ConsoleTaskFactoryTest
    {
        private readonly ServiceProvider _serviceProvider;

        public ConsoleTaskFactoryTest()
        {
            var services = new ServiceCollection()
                .AddLogging()
                .AddAzureDevOpsRestApi(new AzureDevOpsRestApiConfiguration())
                .AddSingleton(new MapperConfiguration(x => { }).CreateMapper());

            _serviceProvider = services.BuildServiceProvider();
        }

        [Theory]
        [InlineData("list", "projects", "ListProjectTask")]
        [InlineData("show", "build", "ShowBuildTask")]
        [InlineData("queue", "build", "QueueBuildTask")]
        [InlineData("list", "builds", "ListBuildTask")]
        [InlineData("list", "artifacts", "ListArtifactTask")]
        [InlineData("create", "release", "CreateReleaseTask")]
        [InlineData("update", "variables", "UpdateVariableTask")]
        public void ConsoleTaskFactoryCreate_ShouldCoverAllOptions(string action, string resource, string taskClassName)
        {
            // Arrange
            var factory = new ConsoleTaskFactory(_serviceProvider);

            // Act
            var task = factory.Create(action, resource, out var errorMessage);

            // Assert
            errorMessage.Should().BeNull();
            task.Should().NotBeNull();
            task.GetType().Name.Should().Be(taskClassName);
        }

        [Theory]
        [InlineData("list", null, "Unknown resource \"\". Available resources: \"projects\", \"build\", \"builds\", \"artifacts\", \"release\", \"variables\"")]
        [InlineData("list", "", "Unknown resource \"\". Available resources: \"projects\", \"build\", \"builds\", \"artifacts\", \"release\", \"variables\"")]
        [InlineData("edit", "projects", "Unknown action \"edit\" for resource \"projects\"")]
        public void ConsoleTaskFactoryCreate_ShouldSetErrorMessageOnInvalidOptions(string action, string resource, string expected)
        {
            // Arrange
            var factory = new ConsoleTaskFactory(_serviceProvider);

            // Act
            var task = factory.Create(action, resource, out var actual);

            // Assert
            task.Should().BeNull();
            actual.Should().NotBeNull();
            actual.Should().Be(expected);
        }
    }
}
