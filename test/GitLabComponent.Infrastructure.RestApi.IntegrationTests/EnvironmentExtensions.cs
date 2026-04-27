using System;

namespace AlmOps.GitLabComponent.Infrastructure.RestApi.IntegrationTests;

public static class EnvironmentExtensions
{
    public static string GetEnvironmentVariable(string environment, string provider, string key)
    {
        var name = $"AlmOps__{environment}__{provider}__{key}";
        var output = Environment.GetEnvironmentVariable(name);
        return string.IsNullOrEmpty(output) ? throw new ArgumentException($"Environment variable \"{name}\" is not defined or empty") : output;
    }
}
