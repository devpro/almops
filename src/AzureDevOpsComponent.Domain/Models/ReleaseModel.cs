using System;
using System.Collections.Generic;

namespace AlmOps.AzureDevOpsComponent.Domain.Models
{
    public class ReleaseModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public PersonModel ModifiedBy { get; set; }
        public PersonModel CreatedBy { get; set; }
        public List<EnvironmentModel> Environments { get; set; }
        public object Variables { get; set; }
        public List<object> VariableGroups { get; set; }
        public ReleaseDefinitionModel ReleaseDefinition { get; set; }
        public string Description { get; set; }
        public string Reason { get; set; }
        public string ReleaseNameFormat { get; set; }
        public bool KeepForever { get; set; }
        public int DefinitionSnapshotRevision { get; set; }
        public string LogsContainerUrl { get; set; }
        public string Url { get; set; }
        public List<object> Tags { get; set; }
        public ProjectModel ProjectReference { get; set; }
        public object Properties { get; set; }
    }
}
