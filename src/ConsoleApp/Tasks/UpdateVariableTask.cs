using System.Linq;
using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace AlmOps.ConsoleApp.Tasks
{
    class UpdateVariableTask : TaskBase
    {
        private readonly ILogger<UpdateVariableTask> _logger;

        private readonly IVariableGroupRepository _variableGroupRepository;

        public UpdateVariableTask(ILogger<UpdateVariableTask> logger, IVariableGroupRepository variableGroupRepository)
        {
            _logger = logger;
            _variableGroupRepository = variableGroupRepository;
        }

        public override async Task<string> ExecuteAsync(CommandLineOptions options)
        {
            if (string.IsNullOrEmpty(options.Project) || string.IsNullOrEmpty(options.Id))
            {
                return null;
            }

            _logger.LogDebug($"Get variable group ID={options.Id} in project \"{options.Project}\"");

            var variableGroup = await _variableGroupRepository.FindOneByIdAsync(options.Project, options.Id);
            if (variableGroup == null)
            {
                _logger.LogWarning($"Cannot find variable group with ID={options.Id}");
                return null;
            }

            _logger.LogDebug($"Update variable group \"{variableGroup.Name}\"");

            await _variableGroupRepository.UpdateAsync(
                options.Project,
                options.Id,
                GetVariables(options.Variables.ToList(), CommandLineOptions.VariableSeparator));

            return $"Variable group \"{variableGroup.Name}\" has been updated";
        }
    }
}
