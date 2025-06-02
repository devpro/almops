using System.Linq;
using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace AlmOps.ConsoleApp.Tasks;

internal class UpdateVariableTask(
    ILogger<UpdateVariableTask> logger,
    IVariableGroupRepository variableGroupRepository)
    : TaskBase
{
    public override async Task<string?> ExecuteAsync(CommandLineOptions options)
    {
        if (string.IsNullOrEmpty(options.Project) || string.IsNullOrEmpty(options.Id))
        {
            return null;
        }

        logger.LogDebug("Get variable group ID={OpenId} in project \"{Project}\"", options.Id, options.Project);

        var variableGroup = await variableGroupRepository.FindOneByIdAsync(options.Project, options.Id);
        if (variableGroup == null)
        {
            logger.LogWarning("Cannot find variable group with ID={OptionsId}", options.Id);
            return null;
        }

        logger.LogDebug("Update variable group \"{VariableGroupName}\"", variableGroup.Name);

        await variableGroupRepository.UpdateAsync(
            options.Project,
            options.Id,
            GetVariables(options.Variables.ToList(), CommandLineOptions.VariableSeparator));

        return $"Variable group \"{variableGroup.Name}\" has been updated";
    }
}
