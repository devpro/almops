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
    public class ProjectRepository : RepositoryBase, IProjectRepository
    {
        #region Constructor

        public ProjectRepository(IAzureDevOpsRestApiConfiguration configuration, ILogger<ProjectRepository> logger, IHttpClientFactory httpClientFactory, IMapper mapper)
            : base(configuration, logger, httpClientFactory, mapper)
        {
        }

        #endregion

        #region RepositoryBase properties

        protected override string ResourceName => "_apis/projects";

        #endregion

        #region IProjectRepository methods

        public async Task<List<ProjectModel>> FindAllAsync()
        {
            var resultList = await GetAsync<ResultListDto<ProjectDto>>(GenerateUrl());
            return Mapper.Map<List<ProjectModel>>(resultList.Value);
        }

        #endregion
    }
}
