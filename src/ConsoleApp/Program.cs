using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.DependencyInjection;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AlmOps.ConsoleApp
{
    class Program
    {
        #region Constants

        private const string _AppsettingsFilename = "appsettings.json";

        #endregion

        private async static Task<int> Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var configuration = LoadConfiguration();

            using (var serviceProvider = CreateServiceProvider(configuration))
            {
                var projectRepository = serviceProvider.GetService<AzureDevOpsComponent.Domain.Repositories.IProjectRepository>();
                var projects = await projectRepository.FindAllAsync();

                Console.WriteLine($"Successful query, {projects.Count} projects found = {string.Join(",", projects.Select(x => x.Name))}");
            }
            return 0;
        }

        private static IConfigurationRoot LoadConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location))
                .AddJsonFile(_AppsettingsFilename, true, true)
                .AddEnvironmentVariables()
                .Build();
        }

        private static ServiceProvider CreateServiceProvider(IConfigurationRoot configuration)
        {
            var serviceCollection = new ServiceCollection()
                .AddLogging(builder =>
                {
                    builder
                        .AddFilter("Microsoft", LogLevel.Information)
                        .AddFilter("System", LogLevel.Information)
                        .AddFilter("AlmOps", LogLevel.Debug)
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
    }
}
