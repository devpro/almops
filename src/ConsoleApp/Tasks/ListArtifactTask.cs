using System.Linq;
using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Domain.Models;
using AlmOps.AzureDevOpsComponent.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Withywoods.System;

namespace AlmOps.ConsoleApp.Tasks;

internal class ListArtifactTask(ILogger<ListArtifactTask> logger, IBuildArtifactRepository buildArtifactRepository)
    : IConsoleTask
{
    public async Task<string> ExecuteAsync(CommandLineOptions options)
    {
        if (string.IsNullOrEmpty(options.Project) || string.IsNullOrEmpty(options.Id))
        {
            return null;
        }

        logger.LogDebug("Query the build artifact repository");

        var artifacts = await buildArtifactRepository.FindAllAsync(options.Project, options.Id);
        if (artifacts.Count == 0)
        {
            return null;
        }

        if (!string.IsNullOrEmpty(options.Query))
        {
            var property = typeof(BuildArtifactModel).GetProperty(options.Query.FirstCharToUpper());
            if (property != null)
            {
                return (string)property.GetValue(artifacts.First());
            }
        }

        return $"Successful query, {artifacts.Count} artifacts found = {string.Join(",", artifacts.Select(x => x.Name + ":" + x.Source))}";
    }
}
