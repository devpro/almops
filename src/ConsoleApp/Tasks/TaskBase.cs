using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlmOps.ConsoleApp.Tasks
{
    public abstract class TaskBase : IConsoleTask
    {
        public abstract Task<string> ExecuteAsync(CommandLineOptions options);

        protected Dictionary<string, string> GetVariables(List<string> inputVariables, string separator)
        {
            var output = new Dictionary<string, string>();

            if (!inputVariables.Any())
            {
                return output;
            }

            inputVariables.ForEach(x => output.Add(
                x.Split(separator)[0],
                x.Contains(separator) ? x.Substring(x.IndexOf(separator) + 1) : null));

            return output;
        }
    }
}
