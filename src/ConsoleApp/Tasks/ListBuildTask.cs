using System.Linq;
using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Domain.Models;
using AlmOps.AzureDevOpsComponent.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Withywoods.System;

namespace AlmOps.ConsoleApp.Tasks
{
    class ListBuildTask : IConsoleTask
    {
        private readonly ILogger<ListBuildTask> _logger;

        private readonly IBuildRepository _buildRepository;

        public ListBuildTask(ILogger<ListBuildTask> logger, IBuildRepository buildRepository)
        {
            _logger = logger;
            _buildRepository = buildRepository;
        }

        public async Task<string> ExecuteAsync(CommandLineOptions options)
        {
            if (string.IsNullOrEmpty(options.Project))
            {
                return null;
            }

            _logger.LogDebug("Query the build repository");

            var builds = await _buildRepository.FindAllAsync(options.Project);
            if (!builds.Any())
            {
                return null;
            }

            if (!string.IsNullOrEmpty(options.Query))
            {
                var property = typeof(BuildModel).GetProperty(options.Query.FirstCharToUpper());
                return (string)property.GetValue(builds.First());
            }

            return $"Successful query, {builds.Count} builds found = {string.Join(",", builds.Select(x => x.Id))}";
        }
    }
}
