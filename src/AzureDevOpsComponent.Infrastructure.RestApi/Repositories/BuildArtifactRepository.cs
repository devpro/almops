using System.Net.Http;
using AlmOps.AzureDevOpsComponent.Domain.Repositories;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Repositories
{
    /// <summary>
    /// Build artifact data repository.
    /// </summary>
    /// <see cref="https://docs.microsoft.com/en-us/rest/api/azure/devops/build/artifacts"/>
    public class BuildArtifactRepository : RepositoryBase, IBuildArtifactRepository
    {
        #region Constructor

        public BuildArtifactRepository(IAzureDevOpsRestApiConfiguration configuration, ILogger<BuildArtifactRepository> logger, IHttpClientFactory httpClientFactory, IMapper mapper)
            : base(configuration, logger, httpClientFactory, mapper)
        {
        }

        #endregion

        #region RepositoryBase properties

        protected override string ResourceName => "_apis/build/builds";

        #endregion

        #region IBuildArtifactRepository methods

        #endregion
    }
}
