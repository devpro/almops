using System.Net.Http;
using AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Mappers;
using AlmOps.Common.Infrastructure.RestApi;
using Microsoft.Extensions.Logging;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Repositories;

public abstract class RepositoryBase(
    AzureDevOpsRestApiConfiguration configuration,
    ILogger logger,
    IHttpClientFactory httpClientFactory,
    AzureDevOpsDtoMapper mapper)
    : HttpRepositoryBase(logger, httpClientFactory)
{
    private AzureDevOpsRestApiConfiguration Configuration { get; } = configuration;

    protected AzureDevOpsDtoMapper Mapper { get; } = mapper;

    protected override string HttpClientName => Configuration.HttpClientName;

    protected abstract string ResourceName { get; }

    protected string GenerateUrl(string prefix = "", string suffix = "", string arguments = "", bool isVsrm = false, bool isPreview = false)
    {
        var baseUrl = isVsrm ? Configuration.BaseUrl.Replace("https://", "https://vsrm.") : Configuration.BaseUrl;
        var previewSuffix = isPreview ? "-preview" : "";
        return $"{baseUrl}{prefix}/{ResourceName}{suffix}?api-version={Configuration.ApiVersion}{previewSuffix}{arguments}";
    }
}
