using System.Linq;
using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace AlmOps.ConsoleApp.Tasks;

internal class CreateReleaseTask(
    ILogger<CreateReleaseTask> logger,
    IReleaseDefinitionRepository releaseDefinitionRepository,
    IBuildRepository buildRepository,
    IReleaseRepository releaseRepository)
    : IConsoleTask
{
    public async Task<string?> ExecuteAsync(CommandLineOptions options)
    {
        if (string.IsNullOrEmpty(options.Project) || (string.IsNullOrEmpty(options.Id) && string.IsNullOrEmpty(options.Name)))
        {
            return null;
        }

        logger.LogDebug("Create a new release");

        var releaseDefinitionId = options.Id;
        if (!string.IsNullOrEmpty(options.Name))
        {
            var releaseDefinitions = await releaseDefinitionRepository.FindAllAsync(options.Project, options.Name);
            if (releaseDefinitions.Count == 0)
            {
                logger.LogWarning("Cannot find a release definition with {OptionsName} name", options.Name);
                return null;
            }

            releaseDefinitionId = releaseDefinitions.First().Id.ToString();
        }

        var releaseDefinition = await releaseDefinitionRepository.FindOneByIdAsync(options.Project, releaseDefinitionId);

        // gets latest build from the specified branch
        var branchName = options.Branch;
        var builds = await buildRepository.FindAllAsync(options.Project, branchName, releaseDefinition.Artifacts.First().BuildDefinitionId);
        if (builds.Count == 0)
        {
            logger.LogWarning("Cannot find a build on {BranchName} branch", branchName);
            return null;
        }

        var release = await releaseRepository.CreateAsync(options.Project, releaseDefinitionId, builds.First().Id, releaseDefinition.Artifacts.First().Alias);

        return release.Id;
    }
}
