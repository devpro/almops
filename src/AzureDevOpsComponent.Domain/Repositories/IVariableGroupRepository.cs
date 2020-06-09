using System.Collections.Generic;
using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Domain.Models;

namespace AlmOps.AzureDevOpsComponent.Domain.Repositories
{
    public interface IVariableGroupRepository
    {
        Task<VariableGroupModel> FindOneByIdAsync(string projectName, string id);

        Task UpdateAsync(string projectName, string id, Dictionary<string, string> input, bool isReplaceAll = false);
    }
}
