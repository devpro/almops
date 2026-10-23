using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Dto
{
    public class VariableGroupDto
    {
        public long Id { get; set; }

        public string? Type { get; set; }

        public string? Name { get; set; }

        public JsonObject? Variables { get; set; }

        public string? Description { get; set; }

        public PersonDto? CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public PersonDto? ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public bool IsShared { get; set; }

        [JsonPropertyName("variableGroupProjectReferences")]
        public List<ProjectReferenceDto>? ProjectReferences { get; set; }

        // providerData VariableGroupProviderData
    }
}
