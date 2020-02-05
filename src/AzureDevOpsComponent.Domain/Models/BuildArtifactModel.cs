namespace AlmOps.AzureDevOpsComponent.Domain.Models
{
    public class BuildArtifactModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public string ResourceType { get; set; }
        public string ResourceData { get; set; }
        public string ResourceUrl { get; set; }
        public string ResourceDownloadUrl { get; set; }
        public string ResourceLocalPath { get; set; }
        public string ResourceArtifactSize { get; set; }
    }
}
