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
    /// Release definition repository.
    /// </summary>
    /// <remarks>
    /// https://docs.microsoft.com/en-us/rest/api/azure/devops/release/definitions?view=azure-devops-rest-5.1
    /// </remarks>
    public class ReleaseDefinitionRepository : RepositoryBase, IReleaseDefinitionRepository
    {
        public ReleaseDefinitionRepository(IAzureDevOpsRestApiConfiguration configuration, ILogger<ReleaseDefinitionRepository> logger, IHttpClientFactory httpClientFactory, IMapper mapper)
            : base(configuration, logger, httpClientFactory, mapper)
        {
        }

        protected override string ResourceName => "_apis/release/definitions";

        public async Task<List<ReleaseDefinitionModel>> FindAllAsync(string projectName, string searchText)
        {
            var arguments = $"&$top=10&searchText={searchText}";
            var resultList = await GetAsync<ResultListDto<ReleaseDefinitionDto>>(
                GenerateUrl(prefix: $"/{projectName}", arguments: arguments, isVsrm: true));
            return Mapper.Map<List<ReleaseDefinitionModel>>(resultList.Value);
        }

        public async Task<ReleaseDefinitionModel> FindOneByIdAsync(string projectName, string id)
        {
            var result = await GetAsync<ReleaseDefinitionDto>(
                GenerateUrl(prefix: $"/{projectName}", suffix: $"/{id}", isVsrm: true));
            return Mapper.Map<ReleaseDefinitionModel>(result);
        }
    }
}
