namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Dto
{
    public class BuildArtifactDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public ResourceDto Resource { get; set; }
    }
}
