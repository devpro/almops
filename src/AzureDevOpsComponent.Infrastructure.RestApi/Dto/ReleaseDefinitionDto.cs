using System;
using System.Collections.Generic;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Dto
{
    public class ReleaseDefinitionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public LinksDto Links { get; set; }
        public string Source { get; set; }
        public int Revision { get; set; }
        public object Description { get; set; }
        public PersonDto CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public PersonDto ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public ReleaseDto LastRelease { get; set; }
        public object Variables { get; set; }
        public List<object> VariableGroups { get; set; }
        public List<EnvironmentDto> Environments { get; set; }
        public List<ReleaseArtifactDto> Artifacts { get; set; }
        public List<object> Triggers { get; set; }
        public string ReleaseNameFormat { get; set; }
        public List<object> Tags { get; set; }
        public object Properties { get; set; }
        public string Path { get; set; }
        public object ProjectReference { get; set; }
    }
}
