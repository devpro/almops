using System.Linq;
using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Domain.Models;
using AlmOps.AzureDevOpsComponent.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Withywoods.System;

namespace AlmOps.ConsoleApp.Tasks;

internal class ListProjectTask(ILogger<ListProjectTask> logger, IProjectRepository projectRepository)
    : IConsoleTask
{
    public async Task<string> ExecuteAsync(CommandLineOptions options)
    {
        logger.LogDebug("Query the project repository");

        var projects = await projectRepository.FindAllAsync();
        if (projects.Count == 0)
        {
            return null;
        }

        if (!string.IsNullOrEmpty(options.Query))
        {
            var property = typeof(ProjectModel).GetProperty(options.Query.FirstCharToUpper());
            if (property != null)
            {
                return (string)property.GetValue(projects.First());
            }
        }

        return $"Successful query, {projects.Count} projects found = {string.Join(",", projects.Select(x => x.Name))}";
    }
}
