using System.Collections.Generic;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Dto
{
    public class EnvironmentDto
    {
        public int Id { get; set; }
        public int ReleaseId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public object Variables { get; set; }
        public object PreDeployApprovals { get; set; }
        public object PostDeployApprovals { get; set; }
        public ApprovalsSnapshotDto PreApprovalsSnapshot { get; set; }
        public ApprovalsSnapshotDto PostApprovalsSnapshot { get; set; }
        public List<object> DeploySteps { get; set; }
        public int Rank { get; set; }
        public int DefinitionEnvironmentId { get; set; }
        public EnvironmentOptionsDto EnvironmentOptions { get; set; }
        public List<object> Demands { get; set; }
        public List<ConditionDto> Conditions { get; set; }
        public List<object> WorkflowTasks { get; set; }
        public List<DeployPhasesSnapshotDto> DeployPhasesSnapshot { get; set; }
        public PersonDto Owner { get; set; }
        public List<object> Schedules { get; set; }
        public ReleaseDto Release { get; set; }
        public ReleaseDefinitionDto ReleaseDefinition { get; set; }
        public PersonDto ReleaseCreatedBy { get; set; }
        public string TriggerReason { get; set; }
    }
}
