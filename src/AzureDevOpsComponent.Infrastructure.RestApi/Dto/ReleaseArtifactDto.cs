namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Dto
{
    public class ReleaseArtifactDto
    {
        public string SourceId { get; set; }
        public string Type { get; set; }
        public string Alias { get; set; }
        public DefinitionReferenceDto DefinitionReference { get; set; }
        public bool IsPrimary { get; set; }
    }
}
