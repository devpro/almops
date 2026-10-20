using System.Net.Http;
using AlmOps.Common.Infrastructure.RestApi;
using Microsoft.Extensions.Logging;

namespace AlmOps.GitLabComponent.Infrastructure.RestApi.Repositories;

public abstract class RepositoryBase(GitLabRestApiConfiguration configuration, ILogger logger, IHttpClientFactory httpClientFactory)
    : HttpRepositoryBase(logger, httpClientFactory)
{
    protected override string HttpClientName => GitLabRestApiConfiguration.HttpClientName;

    protected string GenerateUrl(string resource)
    {
        return $"{configuration.BaseUrl}/api/v4/{resource}";
    }
}
