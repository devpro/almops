using System.Net.Http;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Withywoods.Net.Http;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Repositories;

public abstract class RepositoryBase(
    IAzureDevOpsRestApiConfiguration configuration,
    ILogger logger,
    IHttpClientFactory httpClientFactory,
    IMapper mapper)
    : HttpRepositoryBase(logger, httpClientFactory)
{
    private IAzureDevOpsRestApiConfiguration Configuration { get; set; } = configuration;

    protected IMapper Mapper { get; private set; } = mapper;

    protected override string HttpClientName => Configuration.HttpClientName;

    protected abstract string ResourceName { get; }

    protected string GenerateUrl(string prefix = "", string suffix = "", string arguments = "", bool isVsrm = false, bool isPreview = false)
    {
        var baseUrl = isVsrm ? Configuration.BaseUrl.Replace("https://", "https://vsrm.") : Configuration.BaseUrl;
        var previewSuffix = isPreview ? "-preview" : "";
        return $"{baseUrl}{prefix}/{ResourceName}{suffix}?api-version={Configuration.ApiVersion}{previewSuffix}{arguments}";
    }
}
