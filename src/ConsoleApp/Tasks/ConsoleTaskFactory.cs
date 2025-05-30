using AlmOps.AzureDevOpsComponent.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AlmOps.ConsoleApp.Tasks;

public class ConsoleTaskFactory(ServiceProvider serviceProvider)
{
    public IConsoleTask Create(string action, string resource, out string errorMessage)
    {
        errorMessage = null;
        switch (resource)
        {
            case "projects":
                if (action == "list")
                {
                    return new ListProjectTask(
                        serviceProvider.GetService<ILogger<ListProjectTask>>(),
                        serviceProvider.GetService<IProjectRepository>());
                }
                break;
            case "build":
                if (action == "show")
                {
                    return new ShowBuildTask(
                        serviceProvider.GetService<ILogger<ShowBuildTask>>(),
                        serviceProvider.GetService<IBuildRepository>());
                }
                if (action == "queue")
                {
                    return new QueueBuildTask(
                        serviceProvider.GetService<ILogger<QueueBuildTask>>(),
                        serviceProvider.GetService<IBuildRepository>(),
                        serviceProvider.GetService<IBuildTagRepository>());
                }
                break;
            case "builds":
                if (action == "list")
                {
                    return new ListBuildTask(
                        serviceProvider.GetService<ILogger<ListBuildTask>>(),
                        serviceProvider.GetService<IBuildRepository>());
                }
                break;
            case "artifacts":
                if (action == "list")
                {
                    return new ListArtifactTask(
                        serviceProvider.GetService<ILogger<ListArtifactTask>>(),
                        serviceProvider.GetService<IBuildArtifactRepository>());
                }
                break;
            case "release":
                if (action == "create")
                {
                    return new CreateReleaseTask(
                        serviceProvider.GetService<ILogger<CreateReleaseTask>>(),
                        serviceProvider.GetService<IReleaseDefinitionRepository>(),
                        serviceProvider.GetService<IBuildRepository>(),
                        serviceProvider.GetService<IReleaseRepository>());
                }
                break;
            case "variables":
                if (action == "update")
                {
                    return new UpdateVariableTask(
                        serviceProvider.GetService<ILogger<UpdateVariableTask>>(),
                        serviceProvider.GetService<IVariableGroupRepository>());
                }
                break;
            default:
                errorMessage = $"Unknown resource \"{resource}\". Available resources: \"projects\", \"build\", \"builds\", \"artifacts\", \"release\", \"variables\"";
                return null;
        }
        errorMessage = $"Unknown action \"{action}\" for resource \"{resource}\"";
        return null;
    }
}
