using System.Collections.Generic;
using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Domain.Models;

namespace AlmOps.AzureDevOpsComponent.Domain.Repositories
{
    public interface IBuildRepository
    {
        Task<List<BuildModel>> FindAllAsync(string projectName, string branchName = null, string buildDefinitionsId = null);

        Task<BuildModel> FindOneByIdAsync(string projectName, string id);

        Task<BuildModel> CreateAsync(string projectName, string buildDefinitionId, string sourceBranchName, Dictionary<string, string> variables);
    }
}
