namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi
{
    public class DefaultAzureDevOpsRestApiConfiguration : IAzureDevOpsRestApiConfiguration
    {
        public string BaseUrl { get; set; }

        public string ApiVersion { get; set; } = "7.1";

        public string Username { get; set; }

        public string Token { get; set; }

        public string HttpClientName { get; set; } = "AzureDevOpsRestApiClient";
    }
}
