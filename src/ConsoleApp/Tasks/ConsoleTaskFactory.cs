using AlmOps.AzureDevOpsComponent.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AlmOps.ConsoleApp.Tasks;

public class ConsoleTaskFactory(ServiceProvider serviceProvider)
{
    public IConsoleTask? Create(string action, string resource, out string? errorMessage)
    {
        errorMessage = null;
        switch (resource)
        {
            case "projects":
                if (action == "list")
                {
                    return new ListProjectTask(
                        serviceProvider.GetRequiredService<ILogger<ListProjectTask>>(),
                        serviceProvider.GetRequiredService<IProjectRepository>());
                }
                break;
            case "build":
                if (action == "show")
                {
                    return new ShowBuildTask(
                        serviceProvider.GetRequiredService<ILogger<ShowBuildTask>>(),
                        serviceProvider.GetRequiredService<IBuildRepository>());
                }
                if (action == "queue")
                {
                    return new QueueBuildTask(
                        serviceProvider.GetRequiredService<ILogger<QueueBuildTask>>(),
                        serviceProvider.GetRequiredService<IBuildRepository>(),
                        serviceProvider.GetRequiredService<IBuildTagRepository>());
                }
                break;
            case "builds":
                if (action == "list")
                {
                    return new ListBuildTask(
                        serviceProvider.GetRequiredService<ILogger<ListBuildTask>>(),
                        serviceProvider.GetRequiredService<IBuildRepository>());
                }
                break;
            case "artifacts":
                if (action == "list")
                {
                    return new ListArtifactTask(
                        serviceProvider.GetRequiredService<ILogger<ListArtifactTask>>(),
                        serviceProvider.GetRequiredService<IBuildArtifactRepository>());
                }
                break;
            case "release":
                if (action == "create")
                {
                    return new CreateReleaseTask(
                        serviceProvider.GetRequiredService<ILogger<CreateReleaseTask>>(),
                        serviceProvider.GetRequiredService<IReleaseDefinitionRepository>(),
                        serviceProvider.GetRequiredService<IBuildRepository>(),
                        serviceProvider.GetRequiredService<IReleaseRepository>());
                }
                break;
            case "variables":
                if (action == "update")
                {
                    return new UpdateVariableTask(
                        serviceProvider.GetRequiredService<ILogger<UpdateVariableTask>>(),
                        serviceProvider.GetRequiredService<IVariableGroupRepository>());
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
