using System.Collections.Generic;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Dto
{
    public class ApprovalsSnapshotDto
    {
        public List<ApprovalDto> Approvals { get; set; }
        public ApprovalOptionsDto ApprovalOptions { get; set; }
    }
}
