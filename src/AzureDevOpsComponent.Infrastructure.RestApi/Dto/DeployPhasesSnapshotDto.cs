using System.Collections.Generic;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Dto
{
    public class DeployPhasesSnapshotDto
    {
        public DeploymentInputDto DeploymentInput { get; set; }
        public int Rank { get; set; }
        public string PhaseType { get; set; }
        public string Name { get; set; }
        public List<WorkflowTaskDto> WorkflowTasks { get; set; }
    }
}
