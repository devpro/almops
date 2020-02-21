# almops - Ops tool for ALM solutions

[![Build Status](https://dev.azure.com/devprofr/open-source/_apis/build/status/almops-ci?branchName=master)](https://dev.azure.com/devprofr/open-source/_build/latest?definitionId=24&branchName=master)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=devpro.almops&metric=alert_status)](https://sonarcloud.io/dashboard?id=devpro.almops)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=devpro.almops&metric=coverage)](https://sonarcloud.io/dashboard?id=devpro.almops)
[![Nuget](https://img.shields.io/nuget/v/almops.svg)](https://www.nuget.org/packages/almops)

Command line tool to administrate [ALM (Application lifecycle management)](https://en.wikipedia.org/wiki/Application_lifecycle_management) solutions such as Azure DevOps or GitLab.

Examples:

```bash
# List your projects
almops list projects -p myproject

# Queue a new build
almops queue build -p myproject --id 42

# Show build status
almops show build -p myproject --id 123
```

## User manual

### How to install

As a requirement, you only have to install the latest [.NET Core CLI](https://docs.microsoft.com/en-us/dotnet/core/tools/?tabs=netcore2x) (open source tool with a minimized footprint).

As a .NET global tool, `almops` is installed from the NuGet package:

```bash
dotnet tool install --global almops
```

### How to configure

#### Azure DevOps

A personal access token (PAT) to work with Azure DevOps. If you're not familier with it, the instructions are given in the page [Authenticate access with personal access tokens](https://docs.microsoft.com/en-us/azure/devops/organizations/accounts/use-personal-access-tokens-to-authenticate?view=azure-devops&tabs=preview-page).

* Use the tool `config` action

```bash
almops config --org <organization> --user <username> --token <token>
```

* (alternative) Set environment variables

```dos
SET almops__BaseUrl=https://dev.azure.com/<organization>
SET almops__Username=<username>
SET almops__Token=<token>
```

### How to use

#### Quick start

```bash
# display tool version
almops --version

# display help
almops --help

# configure
almops config --org myorg --user myemailadress --token mysecrettoken

# list
almops list projects
```

#### Examples

```bash
# list all builds
almops list builds -p myproject

# queue a new build
almops queue build -p myproject --id 3 --branch mybranch

# show build information
almops show build -p myproject --id 264

# list build artifacts
almops list artifacts -p myproject --id 90

# create a new release from a feature branch with the release definition id
almops create release -p myproject --id 1 --branch "feature/something-awesome"

# create a new release from master branch with the release name
almops create release -p myproject --name myreleasename
```

### How to uninstall

The tool can be easily uninstalled with:

```bash
dotnet tool uninstall -g almops
```

## Contribue

### Requirements

* [.NET Core SDK](https://dotnet.microsoft.com/download) must be installed

### Local project debug

```bash
# build the .NET solution
dotnet build

# run the console app
dotnet src/ConsoleApp/bin/Debug/netcoreapp3.1/almops.dll --help
```

### Local installation

```bash
# pack the projects
dotnet pack

# install from local package
dotnet tool update -g almops --add-source=src/ConsoleApp/nupkg --version 1.1.0-alpha-000000

# run the tool
almops list projects

# uninstall the tool
dotnet tool uninstall -g almops
```

## References

### Microsoft documentation

* [Azure DevOps Services REST API Reference](https://docs.microsoft.com/en-us/rest/api/azure/devops/)
* [microsoft/azure-devops-auth-samples](https://github.com/microsoft/azure-devops-auth-samples)
* [microsoft/azure-devops-node-api](https://github.com/microsoft/azure-devops-node-api)
