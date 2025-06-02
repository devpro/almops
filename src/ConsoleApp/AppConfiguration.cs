using AlmOps.AzureDevOpsComponent.Infrastructure.RestApi;
using Microsoft.Extensions.Configuration;

namespace AlmOps.ConsoleApp;

public class AppConfiguration(IConfigurationRoot configurationRoot)
{
    public AzureDevOpsRestApiConfiguration AzureDevOpsRestApiConfiguration
    {
        get
        {
            return new AzureDevOpsRestApiConfiguration
            {
                BaseUrl = configurationRoot.GetSection("almops:BaseUrl")?.Value ?? "",
                ApiVersion = configurationRoot.GetSection("almops:ApiVersion")?.Value ?? "7.1",
                Username = configurationRoot.GetSection("almops:Username")?.Value ?? "",
                Token = configurationRoot.GetSection("almops:Token")?.Value ?? ""
            };
        }
    }

    public bool IsValid()
    {
        return !string.IsNullOrEmpty(AzureDevOpsRestApiConfiguration.BaseUrl)
               && !string.IsNullOrEmpty(AzureDevOpsRestApiConfiguration.Token);
    }
}
