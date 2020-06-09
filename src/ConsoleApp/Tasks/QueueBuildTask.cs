using System.Linq;
using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace AlmOps.ConsoleApp.Tasks
{
    class QueueBuildTask : TaskBase
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

        public override async Task<string> ExecuteAsync(CommandLineOptions options)
        {
            if (string.IsNullOrEmpty(options.Project) || string.IsNullOrEmpty(options.Id))
            {
                return null;
            }

            _logger.LogDebug("Queue a new build");

            var build = await _buildRepository.CreateAsync(
                options.Project,
                options.Id,
                options.Branch ?? "master",
                GetVariables(options.Variables.ToList(), CommandLineOptions.VariableSeparator));
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
    }
}
