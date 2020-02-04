namespace AlmOps.AzureDevOpsComponent.Domain.Models
{
    public class BuildDefinitionModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Uri { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public string QueueStatus { get; set; }
        public int Revision { get; set; }
        public ProjectModel Project { get; set; }
    }
}
