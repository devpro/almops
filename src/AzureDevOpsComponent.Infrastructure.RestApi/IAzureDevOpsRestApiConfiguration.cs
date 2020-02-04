namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi
{
    /// <summary>
    /// Configuration of calls to Azure DevOps REST API for a specific user and organization.
    /// </summary>
    public interface IAzureDevOpsRestApiConfiguration
    {
        /// <summary>
        /// Base url.
        /// </summary>
        /// <example>https://dev.azure.com/myorganization</example>
        string BaseUrl { get; }

        /// <summary>
        /// Api version.
        /// </summary>
        /// <example>5.1</example>
        string ApiVersion { get; }

        /// <summary>
        /// Authentication username.
        /// </summary>
        string Username { get; }

        /// <summary>
        /// Authentication token.
        /// This is a Personal Access Token (PAT) that can be generated from the web interface of Azure DevOps.
        /// </summary>
        string Token { get; }

        /// <summary>
        /// HTTP client name.
        /// </summary>
        public string HttpClientName { get; }
    }
}
