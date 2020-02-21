namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Dto
{
    public class ApprovalDto
    {
        public int Id { get; set; }
        public int Rank { get; set; }
        public bool IsAutomated { get; set; }
        public bool IsNotificationOn { get; set; }
        public PersonDto Approver { get; set; }
    }
}
