namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Dto
{
    public class ConditionDto
    {
        public bool Result { get; set; }
        public string Name { get; set; }
        public string ConditionType { get; set; }
        public string Value { get; set; }
    }
}
