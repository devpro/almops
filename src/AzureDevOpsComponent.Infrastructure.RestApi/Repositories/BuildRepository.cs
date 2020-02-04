using System.Collections.Generic;
using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Domain.Models;
using AlmOps.AzureDevOpsComponent.Domain.Repositories;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Repositories
{
    public class BuildRepository : IBuildRepository
    {
        public BuildRepository()
        {
        }

        public Task<List<BuildModel>> FindAllAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
