using System.Collections.Generic;
using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Domain.Models;

namespace AlmOps.AzureDevOpsComponent.Domain.Repositories
{
    public interface IReleaseDefinitionRepository
    {
        Task<List<ReleaseDefinitionModel>> FindAllAsync(string projectName, string searchText);
        Task<ReleaseDefinitionModel> FindOneByIdAsync(string projectName, string id);
    }
}
