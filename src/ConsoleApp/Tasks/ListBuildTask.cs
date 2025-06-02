using System.Linq;
using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Domain.Models;
using AlmOps.AzureDevOpsComponent.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Withywoods.System;

namespace AlmOps.ConsoleApp.Tasks;

internal class ListBuildTask(ILogger<ListBuildTask> logger, IBuildRepository buildRepository)
    : IConsoleTask
{
    public async Task<string?> ExecuteAsync(CommandLineOptions options)
    {
        if (string.IsNullOrEmpty(options.Project))
        {
            return null;
        }

        logger.LogDebug("Query the build repository");

        var builds = await buildRepository.FindAllAsync(options.Project);
        if (builds.Count == 0)
        {
            return null;
        }

        if (!string.IsNullOrEmpty(options.Query))
        {
            var property = typeof(BuildModel).GetProperty(options.Query.FirstCharToUpper());
            if (property != null)
            {
                return (string?)property.GetValue(builds.First());
            }
        }

        return $"Successful query, {builds.Count} builds found = {string.Join(",", builds.Select(x => x.Id))}";
    }
}
