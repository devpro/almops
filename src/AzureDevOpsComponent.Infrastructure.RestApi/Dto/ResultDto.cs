using System.Collections.Generic;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Dto;

public class ResultListDto<T>
{
    public List<T> Value { get; set; }
    public int Count { get; set; }
}
