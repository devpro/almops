using System.Linq;
using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace AlmOps.ConsoleApp.Tasks
{
    class CreateReleaseTask : IConsoleTask
    {
        private readonly ILogger<CreateReleaseTask> _logger;

        private readonly IReleaseDefinitionRepository _releaseDefinitionRepository;

        private readonly IBuildRepository _buildRepository;

        private readonly IReleaseRepository _releaseRepository;

        public CreateReleaseTask(
            ILogger<CreateReleaseTask> logger,
            IReleaseDefinitionRepository releaseDefinitionRepository,
            IBuildRepository buildRepository,
            IReleaseRepository releaseRepository)
        {
            _logger = logger;
            _releaseDefinitionRepository = releaseDefinitionRepository;
            _buildRepository = buildRepository;
            _releaseRepository = releaseRepository;
        }

        public async Task<string> ExecuteAsync(CommandLineOptions options)
        {
            if (string.IsNullOrEmpty(options.Project) || (string.IsNullOrEmpty(options.Id) && string.IsNullOrEmpty(options.Name)))
            {
                return null;
            }

            _logger.LogDebug("Create a new release");

            var releaseDefinitionId = options.Id;
            if (!string.IsNullOrEmpty(options.Name))
            {
                var releaseDefinitions = await _releaseDefinitionRepository.FindAllAsync(options.Project, options.Name);
                if (!releaseDefinitions.Any())
                {
                    _logger.LogWarning($"Cannot find a release definition with {options.Name} name");
                    return null;
                }

                releaseDefinitionId = releaseDefinitions.First().Id.ToString();
            }

            var releaseDefinition = await _releaseDefinitionRepository.FindOneByIdAsync(options.Project, releaseDefinitionId);

            // get latest build from the specified branch, master by default
            var branchName = options.Branch ?? "master";
            var builds = await _buildRepository.FindAllAsync(options.Project, branchName, releaseDefinition.Artifacts.First().BuildDefinitionId);
            if (!builds.Any())
            {
                _logger.LogWarning($"Cannot find a build on {branchName} branch");
                return null;
            }

            var release = await _releaseRepository.CreateAsync(options.Project, releaseDefinitionId, builds.First().Id, releaseDefinition.Artifacts.First().Alias);

            return release.Id;
        }
    }
}
