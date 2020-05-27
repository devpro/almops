using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Domain.Models;
using AlmOps.AzureDevOpsComponent.Domain.Repositories;
using AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Dto;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Repositories
{
    /// <summary>
    /// Build artifact data repository.
    /// </summary>
    /// <remarks>https://docs.microsoft.com/en-us/rest/api/azure/devops/build/artifacts</remarks>
    public class BuildArtifactRepository : RepositoryBase, IBuildArtifactRepository
    {
        public BuildArtifactRepository(
            IAzureDevOpsRestApiConfiguration configuration,
            ILogger<BuildArtifactRepository> logger,
            IHttpClientFactory httpClientFactory,
            IMapper mapper)
            : base(configuration, logger, httpClientFactory, mapper)
        {
        }

        protected override string ResourceName => "_apis/build/builds";

        /// <summary>
        /// Find all artifacts for a build
        /// </summary>
        /// <param name="projectName">Project name</param>
        /// <param name="buildId">Build id</param>
        /// <returns></returns>
        /// <remarks>
        /// GET https://dev.azure.com/{organization}/{project}/_apis/build/builds/{buildId}/artifacts
        /// </remarks>
        public async Task<List<BuildArtifactModel>> FindAllAsync(string projectName, string buildId)
        {
            var resultList = await GetAsync<ResultListDto<BuildArtifactDto>>(GenerateUrl(prefix: $"/{projectName}", suffix: $"/{buildId}/artifacts"));
            return Mapper.Map<List<BuildArtifactModel>>(resultList.Value);
        }
    }
}
