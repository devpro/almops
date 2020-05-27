using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace AlmOps.ConsoleApp.Tasks
{
    class QueueBuildTask : IConsoleTask
    {
        private readonly ILogger<QueueBuildTask> _logger;

        private readonly IBuildRepository _buildRepository;

        private readonly IBuildTagRepository _buildTagRepository;

        public QueueBuildTask(ILogger<QueueBuildTask> logger, IBuildRepository buildRepository, IBuildTagRepository buildTagRepository)
        {
            _logger = logger;
            _buildRepository = buildRepository;
            _buildTagRepository = buildTagRepository;
        }

        public async Task<string> ExecuteAsync(CommandLineOptions options)
        {
            if (string.IsNullOrEmpty(options.Project) || string.IsNullOrEmpty(options.Id))
            {
                return null;
            }

            _logger.LogDebug("Queue a new build");

            var build = await _buildRepository.CreateAsync(options.Project, options.Id, options.Branch ?? "master", GetBuildVariables(options));
            if (build == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(options.Tag))
            {
                await _buildTagRepository.AddOneAsync(options.Project, build.Id, options.Tag);
            }

            return build.Id;
        }

        private static Dictionary<string, string> GetBuildVariables(CommandLineOptions options)
        {
            var output = new Dictionary<string, string>();

            var inputVariables = options.Variables.ToList();
            if (!inputVariables.Any())
            {
                return output;
            }

            inputVariables.ForEach(x => output.Add(
                x.Split(CommandLineOptions.VariableSeparator)[0],
                x.Contains(CommandLineOptions.VariableSeparator) ? x.Split(CommandLineOptions.VariableSeparator)[1] : null));

            return output;
        }
    }
}
