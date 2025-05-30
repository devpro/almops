# almops - Ops tool for ALM solutions

[![GitLab Pipeline Status](https://gitlab.com/devpro-labs/software/almops/badges/main/pipeline.svg)](https://gitlab.com/devpro-labs/software/almops/-/pipelines)
[![GitLab Coverage Report](https://gitlab.com/devpro-labs/software/almops/badges/main/coverage.svg)](https://gitlab.com/devpro-labs/software/almops/-/commits/main)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=devpro.almops&metric=alert_status)](https://sonarcloud.io/dashboard?id=devpro.almops)
[![Nuget](https://img.shields.io/nuget/v/almops.svg)](https://www.nuget.org/packages/almops)
[![Docker Image Version](https://img.shields.io/docker/v/devprofr/almops?label=Docker)](https://hub.docker.com/r/devprofr/almops)

<!--[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=devpro.almops&metric=coverage)](https://sonarcloud.io/dashboard?id=devpro.almops)-->


Command line tool to administrate [ALM (Application lifecycle management)](https://en.wikipedia.org/wiki/Application_lifecycle_management) solutions such as Azure DevOps, GitHub, GitLab.

## How to install

### Option 1 - .NET global tool

* Install [.NET CLI](https://learn.microsoft.com/en-us/dotnet/core/install/)
* Install `almops` globally

```bash
dotnet tool install --global almops
```

* If needed, uninstall `almops`

```bash
dotnet tool uninstall -g almops
```

### Option 2 - Container

```bash
docker run -it --rm --name almops \
  -e almops__BaseUrl="https://dev.azure.com/***" -e almops__Username="***" -e almops__Token="***" \
  devprofr/almops:latest list projects
```

## How to authenticate

### Azure DevOps

Create a Personal Access Token (PAT) in your Azure DevOps organization (ref. [Authenticate access with personal access tokens](https://learn.microsoft.com/en-us/azure/devops/organizations/accounts/use-personal-access-tokens-to-authenticate)).

## How to configure

### Option 1 - Use config action

```bash
almops config --org <organization> --user <username> --token <token>
```

### Option 2 - Set environment variables

```dos
SET almops__BaseUrl=<url>
SET almops__Username=<username>
SET almops__Token=<token>
```

## How to use

### Quick start

```bash
# displays tool version
almops --version

# displays help
almops --help

# configures
almops config --org https://dev.azure.com/<organization> --user <email_address> --token <token>

# lists
almops list projects
```

### Examples

```bash
# lists all builds
almops list builds -p myproject

# queues a new build
almops queue build -p myproject --id 3 --branch mybranch

# shows build information
almops show build -p myproject --id 264

# lists build artifacts
almops list artifacts -p myproject --id 90

# creates a new release from a feature branch with the release definition id
almops create release -p myproject --id 1 --branch "feature/something-awesome"

# creates a new release from main branch with the release name
almops create release -p myproject --name myreleasename
```

## How to contribute

Follow the [Contributing guide](CONTRIBUTING.md).
