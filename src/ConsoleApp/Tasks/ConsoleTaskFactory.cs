using AlmOps.AzureDevOpsComponent.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AlmOps.ConsoleApp.Tasks
{
    public class ConsoleTaskFactory
    {
        private readonly ServiceProvider _serviceProvider;

        public ConsoleTaskFactory(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IConsoleTask Create(string action, string resource, out string errorMessage)
        {
            errorMessage = null;
            switch (resource)
            {
                case "projects":
                    if (action == "list")
                    {
                        return new ListProjectTask(
                            _serviceProvider.GetService<ILogger<ListProjectTask>>(),
                            _serviceProvider.GetService<IProjectRepository>());
                    }
                    break;
                case "build":
                    if (action == "show")
                    {
                        return new ShowBuildTask(
                            _serviceProvider.GetService<ILogger<ShowBuildTask>>(),
                            _serviceProvider.GetService<IBuildRepository>());
                    }
                    else if (action == "queue")
                    {
                        return new QueueBuildTask(
                            _serviceProvider.GetService<ILogger<QueueBuildTask>>(),
                            _serviceProvider.GetService<IBuildRepository>(),
                            _serviceProvider.GetService<IBuildTagRepository>());
                    }
                    break;
                case "builds":
                    if (action == "list")
                    {
                        return new ListBuildTask(
                            _serviceProvider.GetService<ILogger<ListBuildTask>>(),
                            _serviceProvider.GetService<IBuildRepository>());
                    }
                    break;
                case "artifacts":
                    if (action == "list")
                    {
                        return new ListArtifactTask(
                            _serviceProvider.GetService<ILogger<ListArtifactTask>>(),
                            _serviceProvider.GetService<IBuildArtifactRepository>());
                    }
                    break;
                case "release":
                    if (action == "create")
                    {
                        return new CreateReleaseTask(
                            _serviceProvider.GetService<ILogger<CreateReleaseTask>>(),
                            _serviceProvider.GetService<IReleaseDefinitionRepository>(),
                            _serviceProvider.GetService<IBuildRepository>(),
                            _serviceProvider.GetService<IReleaseRepository>());
                    }
                    break;
            }
            errorMessage = $"Unknown action \"{action}\" on resource \"{resource}\"";
            return null;
        }
    }
}
