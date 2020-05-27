using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Domain.Models;
using AlmOps.AzureDevOpsComponent.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Withywoods.System;

namespace AlmOps.ConsoleApp.Tasks
{
    class ShowBuildTask : IConsoleTask
    {
        private readonly ILogger<ShowBuildTask> _logger;

        private readonly IBuildRepository _buildRepository;

        public ShowBuildTask(ILogger<ShowBuildTask> logger, IBuildRepository buildRepository)
        {
            _logger = logger;
            _buildRepository = buildRepository;
        }

        public async Task<string> ExecuteAsync(CommandLineOptions options)
        {
            if (string.IsNullOrEmpty(options.Project) || string.IsNullOrEmpty(options.Id))
            {
                return null;
            }

            _logger.LogDebug("Show a build");

            var build = await _buildRepository.FindOneByIdAsync(options.Project, options.Id);
            if (build == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(options.Query))
            {
                var property = typeof(BuildModel).GetProperty(options.Query.FirstCharToUpper());
                return (string)property.GetValue(build);
            }

            return $"Successful query, {build.Id} has a status {build.Status}";
        }
    }
}
