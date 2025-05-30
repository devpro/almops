using AlmOps.AzureDevOpsComponent.Infrastructure.RestApi;
using Microsoft.Extensions.Configuration;

namespace AlmOps.ConsoleApp;

public class AppConfiguration(IConfigurationRoot configurationRoot) : IAzureDevOpsRestApiConfiguration
{
    string IAzureDevOpsRestApiConfiguration.BaseUrl => configurationRoot.GetSection("almops:BaseUrl")?.Value;

    string IAzureDevOpsRestApiConfiguration.ApiVersion => configurationRoot.GetSection("almops:ApiVersion")?.Value ?? "7.1";

    string IAzureDevOpsRestApiConfiguration.Username => configurationRoot.GetSection("almops:Username")?.Value;

    string IAzureDevOpsRestApiConfiguration.Token => configurationRoot.GetSection("almops:Token")?.Value;

    string IAzureDevOpsRestApiConfiguration.HttpClientName => "AzureDevOpsRestApi";

    public bool IsValid()
    {
        return !string.IsNullOrEmpty(((IAzureDevOpsRestApiConfiguration)this).BaseUrl)
               && !string.IsNullOrEmpty(((IAzureDevOpsRestApiConfiguration)this).Token);
    }
}
