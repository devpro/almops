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
    /// Project repository.
    /// </summary>
    /// <remarks>
    /// https://learn.microsoft.com/en-us/rest/api/azure/devops/core/projects
    /// </remarks>
    public class ProjectRepository(
        IAzureDevOpsRestApiConfiguration configuration,
        ILogger<ProjectRepository> logger,
        IHttpClientFactory httpClientFactory,
        IMapper mapper)
        : RepositoryBase(configuration, logger, httpClientFactory, mapper), IProjectRepository
    {
        protected override string ResourceName => "_apis/projects";

        public async Task<List<ProjectModel>> FindAllAsync()
        {
            var resultList = await GetAsync<ResultListDto<ProjectDto>>(GenerateUrl());
            return Mapper.Map<List<ProjectModel>>(resultList.Value);
        }
    }
}
