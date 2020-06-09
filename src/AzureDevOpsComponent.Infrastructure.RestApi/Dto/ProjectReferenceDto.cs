namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Dto
{
    public class ProjectReferenceDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ReferenceDto ProjectReference { get; set; }
    }
}
