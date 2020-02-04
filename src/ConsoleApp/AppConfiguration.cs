using AlmOps.AzureDevOpsComponent.Infrastructure.RestApi;
using Microsoft.Extensions.Configuration;

namespace AlmOps.ConsoleApp
{
    public class AppConfiguration : IAzureDevOpsRestApiConfiguration
    {
        #region Constructor & private fields

        private readonly IConfigurationRoot _configurationRoot;

        public AppConfiguration(IConfigurationRoot configurationRoot)
        {
            _configurationRoot = configurationRoot;
        }

        #endregion

        #region IAzureDevOpsRestApiConfiguration properties

        string IAzureDevOpsRestApiConfiguration.BaseUrl => _configurationRoot.GetSection("almops:BaseUrl")?.Value;

        string IAzureDevOpsRestApiConfiguration.ApiVersion => _configurationRoot.GetSection("almops:ApiVersion")?.Value ?? "5.1";

        string IAzureDevOpsRestApiConfiguration.Username => _configurationRoot.GetSection("almops:Username")?.Value;

        string IAzureDevOpsRestApiConfiguration.Token => _configurationRoot.GetSection("almops:Token")?.Value;

        string IAzureDevOpsRestApiConfiguration.HttpClientName => "AzureDevOpsRestApi";

        #endregion
    }
}
