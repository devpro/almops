namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi
{
    public class AzureDevOpsRestApiConfiguration
    {
        /// <summary>
        /// Base URL
        /// </summary>
        /// <example>https://dev.azure.com/myorganization</example>
        public string BaseUrl { get; init; }

        /// <summary>
        /// API version
        /// </summary>
        /// <example>5.1</example>
        public string ApiVersion { get; set; } = "7.1";

        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; init; }

        /// <summary>
        /// Authentication token.
        /// This is a Personal Access Token (PAT) that can be generated from the web interface of Azure DevOps.
        /// </summary>
        public string Token { get; init; }

        /// <summary>
        /// HTTP Client name
        /// </summary>
        public string HttpClientName { get; } = "AzureDevOpsRestApiClient";
    }
}
