namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Dto
{
    public class InputsDto
    {
        public string ScriptType { get; set; }
        public string ScriptName { get; set; }
        public string Arguments { get; set; }
        public string InlineScript { get; set; }
        public string WorkingFolder { get; set; }
        public string FailOnStandardError { get; set; }
    }
}
