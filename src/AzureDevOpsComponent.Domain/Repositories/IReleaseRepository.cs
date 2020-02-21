using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Domain.Models;

namespace AlmOps.AzureDevOpsComponent.Domain.Repositories
{
    public interface IReleaseRepository
    {
        Task<ReleaseModel> CreateAsync(string projectName, string releaseDefinitionId, string buildId, string alias);
    }
}
