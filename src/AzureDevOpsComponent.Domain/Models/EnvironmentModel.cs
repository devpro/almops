using System.Collections.Generic;

namespace AlmOps.AzureDevOpsComponent.Domain.Models
{
    public class EnvironmentModel
    {
        public int Id { get; set; }

        public int ReleaseId { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public object Variables { get; set; }

        public int Rank { get; set; }

        public int DefinitionEnvironmentId { get; set; }

        public PersonModel Owner { get; set; }

        public List<object> Schedules { get; set; }

        public ReleaseModel Release { get; set; }

        public ReleaseDefinitionModel ReleaseDefinition { get; set; }

        public PersonModel ReleaseCreatedBy { get; set; }

        public string TriggerReason { get; set; }
    }
}
