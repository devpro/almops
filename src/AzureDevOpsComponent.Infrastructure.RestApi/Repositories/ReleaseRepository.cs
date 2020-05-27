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
    /// Release repository.
    /// </summary>
    /// <remarks>https://docs.microsoft.com/en-us/rest/api/azure/devops/release/releases</remarks>
    public class ReleaseRepository : RepositoryBase, IReleaseRepository
    {
        public ReleaseRepository(
            IAzureDevOpsRestApiConfiguration configuration,
            ILogger<ReleaseRepository> logger,
            IHttpClientFactory httpClientFactory,
            IMapper mapper)
            : base(configuration, logger, httpClientFactory, mapper)
        {
        }

        protected override string ResourceName => "_apis/release/releases";

        public async Task<ReleaseModel> CreateAsync(string projectName, string releaseDefinitionId, string buildId, string alias)
        {
            var result = await PostAsync<ReleaseDto>(
                GenerateUrl(prefix: $"/{projectName}", isVsrm: true),
                new
                {
                    DefinitionId = releaseDefinitionId,
                    Description = "Created by almops command line tool",
                    Artifacts = new[]
                    {
                        new
                        {
                            Alias = alias,
                            InstanceReference = new
                            {
                                Id = buildId
                            }
                        }
                    },
                    IsDraft = false,
                    Reason = "none"
                });
            return Mapper.Map<ReleaseModel>(result);
        }
    }
}
