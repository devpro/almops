using System;
using System.Net.Http;
using AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.DependencyInjection;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.UnitTests.DependencyInjection
{
    [Trait("Category", "UnitTests")]
    public class ServiceCollectionExtensionsTest
    {
        private readonly DefaultAzureDevOpsRestApiConfiguration _configuration;

        public ServiceCollectionExtensionsTest()
        {
            _configuration = new DefaultAzureDevOpsRestApiConfiguration();
        }

        [Fact]
        public void AddAzureDevOpsRestApi_ShouldProvideConfiguration()
        {
            // Arrange
            var serviceCollection = new ServiceCollection()
                .AddLogging();

            // Act
            serviceCollection.AddAzureDevOpsRestApi(_configuration);

            // Assert
            var services = serviceCollection.BuildServiceProvider();
            services.GetRequiredService<IAzureDevOpsRestApiConfiguration>().Should().Be(_configuration);
        }

        [Fact]
        public void AddAzureDevOpsRestApi_ShouldProvideRepositories()
        {
            // Arrange
            var mappingConfig = new MapperConfiguration(x => { x.AllowNullCollections = true; });
            var serviceCollection = new ServiceCollection()
                .AddLogging()
                .AddSingleton(mappingConfig.CreateMapper());

            // Act
            serviceCollection.AddAzureDevOpsRestApi(_configuration);

            // Assert
            var services = serviceCollection.BuildServiceProvider();
            services.GetRequiredService<Domain.Repositories.IBuildArtifactRepository>().Should().NotBeNull();
            services.GetRequiredService<Domain.Repositories.IBuildRepository>().Should().NotBeNull();
            services.GetRequiredService<Domain.Repositories.IBuildTagRepository>().Should().NotBeNull();
            services.GetRequiredService<Domain.Repositories.IProjectRepository>().Should().NotBeNull();
            services.GetRequiredService<Domain.Repositories.IReleaseRepository>().Should().NotBeNull();
            services.GetRequiredService<Domain.Repositories.IReleaseDefinitionRepository>().Should().NotBeNull();
        }

        [Fact]
        public void AddAzureDevOpsRestApi_ShouldprovideHttpClient()
        {
            // Arrange
            var serviceCollection = new ServiceCollection()
                .AddLogging();

            // Act
            serviceCollection.AddAzureDevOpsRestApi(_configuration);

            // Assert
            var services = serviceCollection.BuildServiceProvider();
            var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
            httpClientFactory.Should().NotBeNull();
            var client = httpClientFactory.CreateClient(_configuration.HttpClientName);
            client.Should().NotBeNull();
        }

        [Fact]
        public void AddAzureDevOpsRestApi_ShouldThrowExceptionIfServiceCollectionIsNull()
        {
            // Arrange
            var serviceCollection = (ServiceCollection)null;

            // Act
            var exc = Assert.Throws<ArgumentNullException>(() => serviceCollection.AddAzureDevOpsRestApi(_configuration));

            // Assert
            exc.Should().NotBeNull();
            exc.Message.Should().Be("Value cannot be null. (Parameter 'services')");
        }

        [Fact]
        public void AddAzureDevOpsRestApi_ShouldThrowExceptionIfConfigurationIsNull()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            var configuration = (IAzureDevOpsRestApiConfiguration)null;

            // Act
            var exc = Assert.Throws<ArgumentNullException>(() => serviceCollection.AddAzureDevOpsRestApi(configuration));

            // Assert
            exc.Should().NotBeNull();
            exc.Message.Should().Be("Value cannot be null. (Parameter 'configuration')");
        }
    }
}
