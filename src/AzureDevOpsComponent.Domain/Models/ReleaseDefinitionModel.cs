using System;
using System.Collections.Generic;

namespace AlmOps.AzureDevOpsComponent.Domain.Models
{
    public class ReleaseDefinitionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Source { get; set; }
        public int Revision { get; set; }
        public object Description { get; set; }
        public PersonModel CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public PersonModel ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public ReleaseModel LastRelease { get; set; }
        public List<EnvironmentModel> Environments { get; set; }
        public List<ReleaseArtifactModel> Artifacts { get; set; }
        public string ReleaseNameFormat { get; set; }
        public string Path { get; set; }
    }
}
