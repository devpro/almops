using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Domain.Models;
using AlmOps.AzureDevOpsComponent.Infrastructure.RestApi;
using AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.DependencyInjection;
using AlmOps.ConsoleApp.Extensions;
using AutoMapper;
using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Withywoods.Serialization.Json;

namespace AlmOps.ConsoleApp
{
    /// <summary>
    /// Console application entry point.
    /// </summary>
    static class Program
    {
        #region Constants

        private const string _AppsettingsFilename = "appsettings.json";

        #endregion

        #region Entry point

        /// <summary>
        /// Method providing the very entry point.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private async static Task<int> Main(string[] args)
        {
            return await Parser.Default.ParseArguments<CommandLineOptions>(args)
                .MapResult(
                    (CommandLineOptions opts) => RunOptionsAndReturnExitCode(opts),
                    errs => Task.FromResult(HandleParseError(errs))
                );
        }

        #endregion

        #region Private helpers

        private async static Task<int> RunOptionsAndReturnExitCode(CommandLineOptions opts)
        {
            if (opts.Action == "config")
            {
                if (string.IsNullOrEmpty(opts.Organization) || string.IsNullOrEmpty(opts.Username) || string.IsNullOrEmpty(opts.Token))
                {
                    Console.WriteLine("Invalid arguments. Organization, username and token must be set to have a valid configuration.");
                    return -1;
                }
                SaveSettings(opts);
                return 0;
            }

            var configuration = LoadConfiguration();
            var appConfiguration = new AppConfiguration(configuration);
            if (string.IsNullOrEmpty(((IAzureDevOpsRestApiConfiguration)appConfiguration).BaseUrl)
                || string.IsNullOrEmpty(((IAzureDevOpsRestApiConfiguration)appConfiguration).Username)
                || string.IsNullOrEmpty(((IAzureDevOpsRestApiConfiguration)appConfiguration).Token))
            {
                Console.WriteLine("Missing configuration. Please use the config command or set the keys manually.");
                return -1;
            }

            using (var serviceProvider = CreateServiceProvider(opts, configuration))
            {
                switch (opts.Action)
                {
                    case "list":
                        if (string.IsNullOrEmpty(opts.Resource))
                        {
                            Console.WriteLine("The resource must be specified");
                            return -1;
                        }

                        if (opts.Resource == "projects")
                        {
                            LogVerbose(opts, "Query the project collection");

                            var projectRepository = serviceProvider.GetService<AzureDevOpsComponent.Domain.Repositories.IProjectRepository>();
                            var projects = await projectRepository.FindAllAsync();

                            if (!string.IsNullOrEmpty(opts.Query))
                            {
                                var property = typeof(ProjectModel).GetProperty(opts.Query.FirstCharToUpper());
                                Console.WriteLine(property.GetValue(projects.First()));
                            }
                            else
                            {
                                Console.WriteLine($"Successful query, {projects.Count} projects found = {string.Join(",", projects.Select(x => x.Name))}");
                            }
                        }
                        else if (opts.Resource == "builds")
                        {
                            LogVerbose(opts, "Query the build collection");

                            var buildRepository = serviceProvider.GetService<AzureDevOpsComponent.Domain.Repositories.IBuildRepository>();
                            var builds = await buildRepository.FindAllAsync(opts.Project);

                            Console.WriteLine($"Successful query, {builds.Count} builds found = {string.Join(",", builds.Select(x => x.Id))}");
                        }
                        else if (opts.Resource == "artifacts")
                        {
                            LogVerbose(opts, "Query the build artifact collection");

                            var buildArtifactRepository = serviceProvider.GetService<AzureDevOpsComponent.Domain.Repositories.IBuildArtifactRepository>();
                            var artifacts = await buildArtifactRepository.FindAllAsync(opts.Project, opts.Id);

                            Console.WriteLine($"Successful query, {artifacts.Count} artifacts found = {string.Join(",", artifacts.Select(x => x.Name + ":" + x.Source))}");
                        }
                        else
                        {
                            Console.WriteLine($"Unknown resource \"{opts.Resource}\"");
                            return -1;
                        }
                        break;
                    case "show":
                        if (string.IsNullOrEmpty(opts.Resource))
                        {
                            Console.WriteLine("The resource must be specified");
                            return -1;
                        }

                        if (opts.Resource == "build")
                        {
                            LogVerbose(opts, "Show a build");

                            var buildRepository = serviceProvider.GetService<AzureDevOpsComponent.Domain.Repositories.IBuildRepository>();
                            var build = await buildRepository.FindOneByIdAsync(opts.Project, opts.Id);

                            Console.WriteLine($"Successful query, {build.Id} has a status {build.Status}");
                        }
                        else
                        {
                            Console.WriteLine($"Unknown resource \"{opts.Resource}\"");
                            return -1;
                        }
                        break;
                    case "queue":
                        if (string.IsNullOrEmpty(opts.Resource))
                        {
                            Console.WriteLine("The resource must be specified");
                            return -1;
                        }

                        if (opts.Resource == "build")
                        {
                            LogVerbose(opts, "Queue a new build");

                            var buildRepository = serviceProvider.GetService<AzureDevOpsComponent.Domain.Repositories.IBuildRepository>();
                            var build = await buildRepository.CreateAsync(opts.Project, opts.Id);

                            Console.WriteLine(build.Id);
                        }
                        else
                        {
                            Console.WriteLine($"Unknown resource \"{opts.Resource}\"");
                            return -1;
                        }
                        break;
                    default:
                        Console.WriteLine($"Unknown action \"{opts.Action}\"");
                        return -1;
                }
            }
            return 0;
        }

        private static int HandleParseError(IEnumerable<Error> errs)
        {
            var firstTag = errs.FirstOrDefault()?.Tag ?? default;
            if (firstTag == ErrorType.VersionRequestedError || firstTag == ErrorType.HelpRequestedError)
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
                    Username = opts.Username,
                    Token = opts.Token
                }
            };
            File.WriteAllText(Path.Combine(AppContext.BaseDirectory, _AppsettingsFilename), jsonConfig.ToJson(), Encoding.UTF8);
        }

        private static IConfigurationRoot LoadConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location))
                .AddJsonFile(_AppsettingsFilename, true, true)
                .AddEnvironmentVariables()
                .Build();
        }

        private static ServiceProvider CreateServiceProvider(CommandLineOptions opts, IConfigurationRoot configuration)
        {
            LogVerbose(opts, "Create the service provider");
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
                .AddAzureDevOpsRestApi(new AppConfiguration(configuration));

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

        #endregion
    }
}
