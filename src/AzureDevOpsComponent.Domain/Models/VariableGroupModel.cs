namespace AlmOps.AzureDevOpsComponent.Domain.Models
{
    public class VariableGroupModel
    {
        public long Id { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public object Variables { get; set; }
    }
}
