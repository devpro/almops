﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Domain.Models;
using AlmOps.AzureDevOpsComponent.Domain.Repositories;
using AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Dto;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Repositories
{
    public class BuildRepository : RepositoryBase, IBuildRepository
    {
        #region Constructor

        public BuildRepository(IAzureDevOpsRestApiConfiguration configuration, ILogger<BuildRepository> logger, IHttpClientFactory httpClientFactory, IMapper mapper)
            : base(configuration, logger, httpClientFactory, mapper)
        {
        }

        #endregion

        #region RepositoryBase properties

        protected override string ResourceName => "_apis/build/builds";

        #endregion

        #region IProjectRepository methods

        public async Task<List<BuildModel>> FindAllAsync(string projectName)
        {
            var resultList = await GetAsync<ResultListDto<BuildDto>>(GenerateUrl(prefix: $"/{projectName}"));
            return Mapper.Map<List<BuildModel>>(resultList.Value);
        }

        public async Task<BuildModel> FindOneByIdAsync(string projectName, string id)
        {
            var result = await GetAsync<BuildDto>(GenerateUrl(prefix: $"/{projectName}", suffix: $"/{id}"));
            return Mapper.Map<BuildModel>(result);
        }

        public async Task<BuildModel> CreateAsync(string projectName, string buildDefinitionId)
        {
            var result = await PostAsync<BuildDto>(GenerateUrl(prefix: $"/{projectName}"), new { Definition = new { Id = buildDefinitionId } });
            return Mapper.Map<BuildModel>(result);
        }

        #endregion
    }
}
