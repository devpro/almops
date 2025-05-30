using System.Threading.Tasks;

namespace AlmOps.ConsoleApp.Tasks;

public interface IConsoleTask
{
    Task<string> ExecuteAsync(CommandLineOptions options);
}
