namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Dto
{
    public class EnvironmentOptionsDto
    {
        public string EmailNotificationType { get; set; }
        public string EmailRecipients { get; set; }
        public bool SkipArtifactsDownload { get; set; }
        public int TimeoutInMinutes { get; set; }
        public bool EnableAccessToken { get; set; }
        public bool PublishDeploymentStatus { get; set; }
    }
}
