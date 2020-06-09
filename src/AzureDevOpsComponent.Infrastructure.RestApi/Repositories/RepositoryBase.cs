using System.Net.Http;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Withywoods.Net.Http;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Repositories
{
    public abstract class RepositoryBase : HttpRepositoryBase
    {
        protected RepositoryBase(
            IAzureDevOpsRestApiConfiguration configuration,
            ILogger logger,
            IHttpClientFactory httpClientFactory,
            IMapper mapper)
            : base(logger, httpClientFactory)
        {
            Configuration = configuration;
            Mapper = mapper;
        }

        protected IAzureDevOpsRestApiConfiguration Configuration { get; private set; }

        protected IMapper Mapper { get; private set; }

        protected override string HttpClientName => Configuration.HttpClientName;

        protected abstract string ResourceName { get; }

        protected string GenerateUrl(string prefix = "", string suffix = "", string arguments = "", bool isVsrm = false, bool isPreview = false)
        {
            var baseUrl = isVsrm ? Configuration.BaseUrl.Replace("https://", "https://vsrm.") : Configuration.BaseUrl;
            var previewSuffix = isPreview ? "-preview" : "";
            return $"{baseUrl}{prefix}/{ResourceName}{suffix}?api-version={Configuration.ApiVersion}{previewSuffix}{arguments}";
        }
    }
}
