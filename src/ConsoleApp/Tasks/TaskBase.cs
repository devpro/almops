namespace AlmOps.ConsoleApp.Tasks;

public abstract class TaskBase : IConsoleTask
{
    public abstract Task<string?> ExecuteAsync(CommandLineOptions options);

    protected static Dictionary<string, string?> GetVariables(List<string> inputVariables, string separator)
    {
        var output = new Dictionary<string, string?>();

        if (inputVariables.Count > 0)
        {
            inputVariables.ForEach(x => output.Add(
                x.Split(separator)[0],
                x.Contains(separator) ? x[(x.IndexOf(separator, StringComparison.Ordinal) + 1)..] : null));
        }

        return output;
    }
}
