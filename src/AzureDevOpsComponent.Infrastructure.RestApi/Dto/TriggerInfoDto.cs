namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Dto
{
    public class TriggerInfoDto
    {
        public string PrNumber { get; set; }
        public string PrSourceBranch { get; set; }
        public string PrSourceSha { get; set; }
        public string PrTitle { get; set; }
        public string PrIsFork { get; set; }
    }
}
