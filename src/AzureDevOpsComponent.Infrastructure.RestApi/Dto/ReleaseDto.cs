using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Dto
{
    public class ReleaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public PersonDto ModifiedBy { get; set; }
        public PersonDto CreatedBy { get; set; }
        public List<EnvironmentDto> Environments { get; set; }
        public object Variables { get; set; }
        public List<object> VariableGroups { get; set; }
        public List<ReleaseArtifactDto> Artifacts { get; set; }
        public ReleaseDefinitionDto ReleaseDefinition { get; set; }
        public string Description { get; set; }
        public string Reason { get; set; }
        public string ReleaseNameFormat { get; set; }
        public bool KeepForever { get; set; }
        public int DefinitionSnapshotRevision { get; set; }
        public string LogsContainerUrl { get; set; }
        public string Url { get; set; }
        [JsonProperty("_links")]
        public LinksDto Links { get; set; }
        public List<object> Tags { get; set; }
        public ProjectDto ProjectReference { get; set; }
        public object Properties { get; set; }
    }
}
