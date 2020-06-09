using System;
using AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.DependencyInjection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.IntegrationTests
{
    public class RepositoryIntegrationTestBase
    {
        private readonly string _environmentName;

        protected RepositoryIntegrationTestBase(string environmentName)
        {
            _environmentName = environmentName;

            Configuration = new DefaultAzureDevOpsRestApiConfiguration
            {
                BaseUrl = "https://dev.azure.com/" + GetEnvironmentVariable("Organization"),
                Username = GetEnvironmentVariable("Username"),
                Token = GetEnvironmentVariable("Token")
            };

            ProjectName = GetEnvironmentVariable("Project");

            var mappingConfig = new MapperConfiguration(x =>
            {
                x.AddProfile(new MappingProfiles.GenericMappingProfile());
                x.AllowNullCollections = true;
            });

            var services = new ServiceCollection()
                .AddLogging()
                .AddSingleton(mappingConfig.CreateMapper())
                .AddAzureDevOpsRestApi(Configuration);

            ServiceProvider = services.BuildServiceProvider();
        }

        protected ServiceProvider ServiceProvider { get; private set; }

        protected IAzureDevOpsRestApiConfiguration Configuration { get; private set; }

        protected string ProjectName { get; private set; }

        protected string GetEnvironmentVariable(string key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var name = $"AlmOps__{_environmentName}__{key}";
            var output = Environment.GetEnvironmentVariable(name);
            if (string.IsNullOrEmpty(output))
            {
                throw new ArgumentException($"Environment variable \"{name}\" is not defined or empty");
            }

            return output;
        }
    }
}
