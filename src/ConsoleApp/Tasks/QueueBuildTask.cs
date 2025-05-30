using System.Linq;
using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace AlmOps.ConsoleApp.Tasks;

internal class QueueBuildTask(
    ILogger<QueueBuildTask> logger,
    IBuildRepository buildRepository,
    IBuildTagRepository buildTagRepository)
    : TaskBase
{
    public override async Task<string> ExecuteAsync(CommandLineOptions options)
    {
        if (string.IsNullOrEmpty(options.Project) || string.IsNullOrEmpty(options.Id))
        {
            return null;
        }

        logger.LogDebug("Queue a new build");

        var build = await buildRepository.CreateAsync(
            options.Project,
            options.Id,
            options.Branch ?? "main",
            GetVariables(options.Variables.ToList(), CommandLineOptions.VariableSeparator));
        if (build == null)
        {
            return null;
        }

        if (!string.IsNullOrEmpty(options.Tag))
        {
            await buildTagRepository.AddOneAsync(options.Project, build.Id, options.Tag);
        }

        return build.Id;
    }
}
