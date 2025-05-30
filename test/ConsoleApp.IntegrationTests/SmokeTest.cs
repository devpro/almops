using FluentAssertions;

namespace AlmOps.ConsoleApp.IntegrationTests;

[Trait("Environment", "Sandbox")]
public class SmokeTest
{
    [Fact]
    public async Task ReadonlyCheckAsync()
    {
        await RunConsole($"almops config --org {GetEnvironmentVariable("Organization")} --user {GetEnvironmentVariable("Username")} --token {GetEnvironmentVariable("Token")}");
        await RunConsole("almops list projects");
        await RunConsole("almops list projects -q Url");
    }

    private static async Task RunConsole(string commandLine)
    {
        var result = await Program.Main(commandLine.Split(' ').Skip(1).ToArray());
        result.Should().Be(0);
    }

    private static string GetEnvironmentVariable(string key)
    {
        ArgumentNullException.ThrowIfNull(key);

        var name = $"AlmOps__Sandbox__{key}";
        var output = Environment.GetEnvironmentVariable(name);
        if (string.IsNullOrEmpty(output))
        {
            throw new ArgumentException($"Environment variable \"{name}\" is not defined or empty");
        }

        return output;
    }
}
