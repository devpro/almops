using System.Collections.Generic;
using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Domain.Models;

namespace AlmOps.AzureDevOpsComponent.Domain.Repositories
{
    public interface IProjectRepository
    {
        Task<List<ProjectModel>> FindAllAsync();
    }
}
