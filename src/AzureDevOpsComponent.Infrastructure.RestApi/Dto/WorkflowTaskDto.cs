namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Dto
{
    public class WorkflowTaskDto
    {
        public string TaskId { get; set; }

        public string Version { get; set; }

        public string Name { get; set; }

        public bool Enabled { get; set; }

        public bool AlwaysRun { get; set; }

        public bool ContinueOnError { get; set; }

        public int TimeoutInMinutes { get; set; }

        public string DefinitionType { get; set; }

        public InputsDto Inputs { get; set; }
    }
}
