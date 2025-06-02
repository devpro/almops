using Microsoft.Extensions.Logging;
using Withywoods.Net.Http;

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
