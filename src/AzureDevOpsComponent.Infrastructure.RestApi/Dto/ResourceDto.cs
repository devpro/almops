namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Dto
{
    public class ResourceDto
    {
        public string Type { get; set; }
        public string Data { get; set; }
        public ResourcePropertiesDto Properties { get; set; }
        public string Url { get; set; }
        public string DownloadUrl { get; set; }
    }
}
