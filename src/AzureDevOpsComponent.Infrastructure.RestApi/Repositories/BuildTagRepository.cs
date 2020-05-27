using System.Net.Http;
using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Domain.Repositories;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Repositories
{
    /// <summary>
    /// Build tag data repository.
    /// </summary>
    /// <remarks>https://docs.microsoft.com/en-us/rest/api/azure/devops/build/tags</remarks>
    public class BuildTagRepository : RepositoryBase, IBuildTagRepository
    {
        public BuildTagRepository(
            IAzureDevOpsRestApiConfiguration configuration,
            ILogger<BuildTagRepository> logger,
            IHttpClientFactory httpClientFactory,
            IMapper mapper)
            : base(configuration, logger, httpClientFactory, mapper)
        {
        }

        protected override string ResourceName => "_apis/build/builds";

        /// <summary>
        /// Adds a tag to a build.
        /// </summary>
        /// <param name="projectName">Project name</param>
        /// <param name="buildId">Build id</param>
        /// <returns></returns>
        /// <remarks>
        /// https://docs.microsoft.com/en-us/rest/api/azure/devops/build/tags/add%20build%20tag?view=azure-devops-rest-5.1
        /// </remarks>
        public async Task AddOneAsync(string projectName, string buildId, string tag)
        {
            var url = GenerateUrl(prefix: $"/{projectName}", suffix: $"/{buildId}/tags/{tag}");
            await PutAsync(url, string.Empty);
        }
    }
}
