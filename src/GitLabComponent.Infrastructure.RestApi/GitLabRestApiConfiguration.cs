namespace AlmOps.GitLabComponent.Infrastructure.RestApi
{
    public class GitLabRestApiConfiguration
    {
        /// <summary>
        /// Base URL of GitLab instance.
        /// Default to "https://gitlab.com".
        /// </summary>
        public string BaseUrl { get; init; } = "https://gitlab.com";

        /// <summary>
        /// Access token.
        /// Can be a User (Personal) Access Token, a Group Access Token, a Project Access Token, a Service Account Token. 
        /// </summary>
        /// <example>glpat-xxxxxx</example>
        public required string Token { get; init; }

        public static string HttpClientName { get => "GitLabRestApiClient"; }
    }
}
