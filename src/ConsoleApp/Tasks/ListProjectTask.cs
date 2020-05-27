using System.Linq;
using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Domain.Models;
using AlmOps.AzureDevOpsComponent.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Withywoods.System;

namespace AlmOps.ConsoleApp.Tasks
{
    class ListProjectTask : IConsoleTask
    {
        private readonly ILogger<ListProjectTask> _logger;

        private readonly IProjectRepository _projectRepository;

        public ListProjectTask(ILogger<ListProjectTask> logger, IProjectRepository projectRepository)
        {
            _logger = logger;
            _projectRepository = projectRepository;
        }

        public async Task<string> ExecuteAsync(CommandLineOptions options)
        {
            _logger.LogDebug("Query the project repository");

            var projects = await _projectRepository.FindAllAsync();
            if (!projects.Any())
            {
                return null;
            }

            if (!string.IsNullOrEmpty(options.Query))
            {
                var property = typeof(ProjectModel).GetProperty(options.Query.FirstCharToUpper());
                return (string)property.GetValue(projects.First());
            }

            return $"Successful query, {projects.Count} projects found = {string.Join(",", projects.Select(x => x.Name))}";
        }
    }
}
