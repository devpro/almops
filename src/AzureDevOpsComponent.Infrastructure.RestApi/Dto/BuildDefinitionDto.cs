using System.Collections.Generic;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Dto
{
    public class BuildDefinitionDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Uri { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public string QueueStatus { get; set; }
        public int Revision { get; set; }
        public ProjectDto Project { get; set; }
        public List<object> Drafts { get; set; }
    }
}
