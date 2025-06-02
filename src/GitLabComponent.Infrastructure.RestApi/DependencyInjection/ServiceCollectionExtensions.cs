using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AlmOps.GitLabComponent.Infrastructure.RestApi.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGitLabRestApi(this IServiceCollection services, GitLabRestApiConfiguration configuration)
    {
        services.AddSingleton(configuration);
        services.TryAddTransient<Repositories.AuditEventRepository>();
        services
            .AddHttpClient(GitLabRestApiConfiguration.HttpClientName, client =>
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", configuration.Token);
            });
        return services;
    }
}
