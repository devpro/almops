using AlmOps.Common.System;
using AlmOps.GitLabComponent.Infrastructure.RestApi.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace AlmOps.GitLabComponent.Infrastructure.RestApi.IntegrationTests
{
    public abstract class RepositoryIntegrationTestBase
    {
        protected ServiceProvider ServiceProvider { get; private set; }

        protected RepositoryIntegrationTestBase(string environmentName)
        {
            var configuration = new GitLabRestApiConfiguration
            {
                Token = EnvironmentExtensions.GetEnvironmentVariable(environmentName, "GitLab", "Token")
            };

            var services = new ServiceCollection()
                .AddLogging()
                .AddGitLabRestApi(configuration);

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
