namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Dto
{
    public class LinksDto
    {
        public HrefDto Self { get; set; }
        public HrefDto Web { get; set; }
        public HrefDto SourceVersionDisplayUri { get; set; }
        public HrefDto Timeline { get; set; }
        public HrefDto Badge { get; set; }
    }
}
