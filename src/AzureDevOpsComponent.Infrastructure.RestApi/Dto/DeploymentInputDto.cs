using System.Collections.Generic;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Dto
{
    public class DeploymentInputDto
    {
        public ParallelExecutionDto ParallelExecution { get; set; }
        public bool SkipArtifactsDownload { get; set; }
        public int TimeoutInMinutes { get; set; }
        public int QueueId { get; set; }
        public List<object> Demands { get; set; }
        public bool EnableAccessToken { get; set; }
    }
}
