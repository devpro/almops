namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Dto
{
    public class DefinitionReferenceDto
    {
        public NameValueDto ArtifactSourceDefinitionUrl { get; set; }
        public NameValueDto DefaultVersionBranch { get; set; }
        public NameValueDto DefaultVersionSpecific { get; set; }
        public NameValueDto DefaultVersionTags { get; set; }
        public NameValueDto DefaultVersionType { get; set; }
        public NameValueDto Definition { get; set; }
        public ProjectDto Project { get; set; }
        public NameValueDto Version { get; set; }
        public NameValueDto ArtifactSourceVersionUrl { get; set; }
    }
}
