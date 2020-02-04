using System;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.DependencyInjection
{
    /// <summary>
    /// Service collection. extensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the dependency injection configuration.
        /// </summary>
        /// <typeparam name="T">Instance of <see cref="IMongoDbAtlasRestApiConfiguration"/></typeparam>
        /// <param name="services">Collection of services that will be completed</param>
        /// <returns></returns>
        public static IServiceCollection AddAzureDevOpsRestApi<T>(this IServiceCollection services, T configuration)
            where T : class, IAzureDevOpsRestApiConfiguration
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAddTransient<IAzureDevOpsRestApiConfiguration, T>();
            services.TryAddTransient<Domain.Repositories.IBuildRepository, Repositories.BuildRepository>();
            services.TryAddTransient<Domain.Repositories.IProjectRepository, Repositories.ProjectRepository>();

            services
                .AddHttpClient(configuration.HttpClientName, client =>
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                            "Basic",
                            Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", configuration.Username, configuration.Token))));

                    });

            return services;
        }
    }
}
