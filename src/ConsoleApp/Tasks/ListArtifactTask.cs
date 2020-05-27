using System.Linq;
using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Domain.Models;
using AlmOps.AzureDevOpsComponent.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Withywoods.System;

namespace AlmOps.ConsoleApp.Tasks
{
    class ListArtifactTask : IConsoleTask
    {
        private readonly ILogger<ListArtifactTask> _logger;

        private readonly IBuildArtifactRepository _buildArtifactRepository;

        public ListArtifactTask(ILogger<ListArtifactTask> logger, IBuildArtifactRepository buildArtifactRepository)
        {
            _logger = logger;
            _buildArtifactRepository = buildArtifactRepository;
        }

        public async Task<string> ExecuteAsync(CommandLineOptions options)
        {
            if (string.IsNullOrEmpty(options.Project) || string.IsNullOrEmpty(options.Id))
            {
                return null;
            }

            _logger.LogDebug("Query the build artifact repository");

            var artifacts = await _buildArtifactRepository.FindAllAsync(options.Project, options.Id);
            if (!artifacts.Any())
            {
                return null;
            }

            if (!string.IsNullOrEmpty(options.Query))
            {
                var property = typeof(BuildArtifactModel).GetProperty(options.Query.FirstCharToUpper());
                return (string)property.GetValue(artifacts.First());
            }

            return $"Successful query, {artifacts.Count} artifacts found = {string.Join(",", artifacts.Select(x => x.Name + ":" + x.Source))}";
        }
    }
}
