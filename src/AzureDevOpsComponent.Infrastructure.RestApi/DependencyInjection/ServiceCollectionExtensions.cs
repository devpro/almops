﻿using System;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.DependencyInjection;

/// <summary>
/// Service collection. extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add the dependency injection configuration.
    /// </summary>
    /// <param name="services">Collection of services that will be completed</param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddAzureDevOpsRestApi(this IServiceCollection services, AzureDevOpsRestApiConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services.TryAddSingleton(configuration);
        services.TryAddTransient<Domain.Repositories.IBuildArtifactRepository, Repositories.BuildArtifactRepository>();
        services.TryAddTransient<Domain.Repositories.IBuildRepository, Repositories.BuildRepository>();
        services.TryAddTransient<Domain.Repositories.IBuildTagRepository, Repositories.BuildTagRepository>();
        services.TryAddTransient<Domain.Repositories.IProjectRepository, Repositories.ProjectRepository>();
        services.TryAddTransient<Domain.Repositories.IReleaseRepository, Repositories.ReleaseRepository>();
        services.TryAddTransient<Domain.Repositories.IReleaseDefinitionRepository, Repositories.ReleaseDefinitionRepository>();
        services.TryAddTransient<Domain.Repositories.IVariableGroupRepository, Repositories.VariableGroupRepository>();

        services
            .AddHttpClient(configuration.HttpClientName, client =>
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = string.IsNullOrEmpty(configuration.Username)
                    ? new AuthenticationHeaderValue(
                        "Bearer", configuration.Token)
                    : new AuthenticationHeaderValue(
                        "Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{configuration.Username}:{configuration.Token}")));
            });

        return services;
    }
}
