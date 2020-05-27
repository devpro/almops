using System.Threading.Tasks;

namespace AlmOps.AzureDevOpsComponent.Domain.Repositories
{
    public interface IBuildTagRepository
    {
        Task AddOneAsync(string projectName, string buildId, string tag);
    }
}
