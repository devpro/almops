using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Dto
{
    public class BuildDto
    {
        public string Id { get; set; }
        public string BuildNumber { get; set; }
        public string Status { get; set; }
        public string Result { get; set; }
        public DateTime QueueTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public string Url { get; set; }
        public List<object> Tags { get; set; }
        public BuildDefinitionDto Definition { get; set; }
        public ProjectDto Project { get; set; }
        public RepositoryDto Repository { get; set; }
        [JsonProperty("_links")]
        public LinksDto Links { get; set; }
        public object Properties { get; set; }
        public List<ValidationResultDto> ValidationResults { get; set; }
        public List<PlanDto> Plans { get; set; }
        public TriggerInfoDto TriggerInfo { get; set; }
        public int BuildNumberRevision { get; set; }
        public string Uri { get; set; }
        public string SourceBranch { get; set; }
        public string SourceVersion { get; set; }
        public QueueDto Queue { get; set; }
        public string Priority { get; set; }
        public string Reason { get; set; }
        public PersonDto RequestedFor { get; set; }
        public PersonDto RequestedBy { get; set; }
        public DateTime LastChangedDate { get; set; }
        public PersonDto LastChangedBy { get; set; }
        public string Parameters { get; set; }
        public OrchestrationPlanDto OrchestrationPlan { get; set; }
        public LogsDto Logs { get; set; }
        public bool KeepForever { get; set; }
        public bool RetainedByRelease { get; set; }
        public object TriggeredByBuild { get; set; }
    }
}
