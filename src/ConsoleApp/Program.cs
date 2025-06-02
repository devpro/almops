using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.DependencyInjection;
using AlmOps.ConsoleApp.Tasks;
using AutoMapper;
using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Withywoods.Serialization.Json;

[assembly: InternalsVisibleTo("AlmOps.ConsoleApp.IntegrationTests")]

namespace AlmOps.ConsoleApp;

internal static class Program
{
    private const string AppSettingsFilename = "appsettings.json";

    /// <summary>
    /// Method providing the very entry point.
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    internal static async Task<int> Main(string[] args)
    {
        return await Parser.Default.ParseArguments<CommandLineOptions>(args)
            .MapResult(
                RunOptionsAndReturnExitCode,
                errs => Task.FromResult(HandleParseError(errs))
            );
    }

    private static async Task<int> RunOptionsAndReturnExitCode(CommandLineOptions opts)
    {
        if (opts.Action == "config")
        {
            if (string.IsNullOrEmpty(opts.Organization) || string.IsNullOrEmpty(opts.Token))
            {
                Console.WriteLine("Invalid arguments. At least organization and token must be set to have a valid configuration.");
                return -1;
            }
            SaveSettings(opts);
            return 0;
        }

        var configuration = LoadConfiguration();
        var appConfiguration = new AppConfiguration(configuration);
        if (!appConfiguration.IsValid())
        {
            Console.WriteLine("Missing configuration. Please use the config command or set the keys manually.");
            return -1;
        }

        await using var serviceProvider = CreateServiceProvider(opts, configuration);

        var factory = new ConsoleTaskFactory(serviceProvider);

        var task = factory.Create(opts.Action, opts.Resource ?? "", out var errorMessage);
        if (task == null)
        {
            Console.WriteLine(errorMessage);
            return -1;
        }

        try
        {
            var output = await task.ExecuteAsync(opts);
            if (string.IsNullOrEmpty(output))
            {
                Console.WriteLine("No data returned");
                return -1;
            }

            Console.WriteLine(output);
            return 0;
        }
        catch (Exception exc)
        {
            Console.WriteLine($"An error occured: {exc.Message}");
            return -2;
        }
    }

    private static int HandleParseError(IEnumerable<Error> errs)
    {
        var firstTag = errs.FirstOrDefault()?.Tag ?? default;
        if (firstTag is ErrorType.VersionRequestedError or ErrorType.HelpRequestedError)
        {
            return 0;
        }

        return -2;
    }

    private static void SaveSettings(CommandLineOptions opts)
    {
        var jsonConfig = new
        {
            almops = new
            {
                BaseUrl = $"https://dev.azure.com/{opts.Organization}",
                opts.Username,
                opts.Token
            }
        };
        File.WriteAllText(Path.Combine(AppContext.BaseDirectory, AppSettingsFilename), jsonConfig.ToJson(), Encoding.UTF8);
    }

    private static IConfigurationRoot LoadConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile(AppSettingsFilename, true, true)
            .AddEnvironmentVariables()
            .Build();
    }

    private static ServiceProvider CreateServiceProvider(CommandLineOptions opts, IConfigurationRoot configuration)
    {
        LogVerbose(opts, "Create the service provider");
        var appConfiguration = new AppConfiguration(configuration);
        var serviceCollection = new ServiceCollection()
            .AddLogging(builder =>
            {
                builder
                    .AddFilter("Microsoft", opts.IsVerbose ? LogLevel.Information : LogLevel.Warning)
                    .AddFilter("System", opts.IsVerbose ? LogLevel.Information : LogLevel.Warning)
                    .AddFilter("AlmOps", opts.IsVerbose ? LogLevel.Debug : LogLevel.Information)
                    .AddConsole();
            })
            .AddSingleton(configuration)
            .AddAzureDevOpsRestApi(appConfiguration.AzureDevOpsRestApiConfiguration);

        ConfigureAutoMapper(serviceCollection);

        return serviceCollection.BuildServiceProvider();
    }

    private static void ConfigureAutoMapper(IServiceCollection serviceCollection)
    {
        var mappingConfig = new MapperConfiguration(x =>
        {
            x.AddProfile(new AzureDevOpsComponent.Infrastructure.RestApi.MappingProfiles.GenericMappingProfile());
            x.AllowNullCollections = true;
        });
        var mapper = mappingConfig.CreateMapper();
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
        serviceCollection.AddSingleton(mapper);
    }

    private static void LogVerbose(CommandLineOptions opts, string message)
    {
        if (opts.IsVerbose)
        {
            Console.WriteLine(message);
        }
    }
}
