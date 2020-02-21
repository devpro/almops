namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Dto
{
    public class ApprovalOptionsDto
    {
        public int? RequiredApproverCount { get; set; }
        public bool ReleaseCreatorCanBeApprover { get; set; }
        public bool AutoTriggeredAndPreviousEnvironmentApprovedCanBeSkipped { get; set; }
        public bool EnforceIdentityRevalidation { get; set; }
        public int? TimeoutInMintues { get; set; }
    }
}
