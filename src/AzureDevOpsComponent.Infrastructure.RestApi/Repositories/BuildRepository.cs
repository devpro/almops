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
    /// Build repository.
    /// </summary>
    /// <remarks>https://docs.microsoft.com/en-us/rest/api/azure/devops/build/builds</remarks>
    public class BuildRepository : RepositoryBase, IBuildRepository
    {
        public BuildRepository(IAzureDevOpsRestApiConfiguration configuration, ILogger<BuildRepository> logger, IHttpClientFactory httpClientFactory, IMapper mapper)
            : base(configuration, logger, httpClientFactory, mapper)
        {
        }

        protected override string ResourceName => "_apis/build/builds";

        public async Task<List<BuildModel>> FindAllAsync(string projectName, string branchName = null, string buildDefinitionsId = null)
        {
            var arguments = $"&queryOrder=startTimeDescending&$top=10";
            if (!string.IsNullOrEmpty(branchName))
            {
                arguments += $"&branchName=refs/heads/{branchName}";
            }
            if (!string.IsNullOrEmpty(buildDefinitionsId))
            {
                arguments += $"&definitions={buildDefinitionsId}";
            }
            var resultList = await GetAsync<ResultListDto<BuildDto>>(GenerateUrl(prefix: $"/{projectName}", arguments: arguments));
            return Mapper.Map<List<BuildModel>>(resultList.Value);
        }

        public async Task<BuildModel> FindOneByIdAsync(string projectName, string id)
        {
            var result = await GetAsync<BuildDto>(GenerateUrl(prefix: $"/{projectName}", suffix: $"/{id}"));
            return Mapper.Map<BuildModel>(result);
        }

        public async Task<BuildModel> CreateAsync(string projectName, string buildDefinitionId, string sourceBranchName)
        {
            var result = await PostAsync<BuildDto>(
                GenerateUrl(prefix: $"/{projectName}"),
                new
                {
                    Definition = new { Id = buildDefinitionId },
                    SourceBranch = sourceBranchName
                });
            return Mapper.Map<BuildModel>(result);
        }
    }
}
