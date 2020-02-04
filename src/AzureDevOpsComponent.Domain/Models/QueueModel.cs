namespace AlmOps.AzureDevOpsComponent.Domain.Models
{
    public class QueueModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PoolModel Pool { get; set; }
    }
}
