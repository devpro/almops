using System;

namespace AlmOps.AzureDevOpsComponent.Domain.Models
{
    public class BuildModel
    {
        public string Id { get; set; }
        public string BuildNumber { get; set; }
        public string Status { get; set; }
        public string Result { get; set; }
        public DateTime QueueTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public string Url { get; set; }
        public BuildDefinitionModel Definition { get; set; }
        public ProjectModel Project { get; set; }
        public string Uri { get; set; }
        public string SourceBranch { get; set; }
        public string SourceVersion { get; set; }
        public QueueModel Queue { get; set; }
        public string Priority { get; set; }
        public string Reason { get; set; }
        public PersonModel RequestedFor { get; set; }
        public PersonModel RequestedBy { get; set; }
        public DateTime LastChangedDate { get; set; }
        public PersonModel LastChangedBy { get; set; }
    }
}
