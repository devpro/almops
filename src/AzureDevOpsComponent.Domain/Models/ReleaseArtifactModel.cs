namespace AlmOps.AzureDevOpsComponent.Domain.Models
{
    public class ReleaseArtifactModel
    {
        public string SourceId { get; set; }

        public string Type { get; set; }

        public string Alias { get; set; }

        public string BuildDefinitionId { get; set; }

        public string BuildDefinitionName { get; set; }

        public bool IsPrimary { get; set; }
    }
}
