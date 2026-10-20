# Contributing

## Requirements

* [.NET SDK](https://dotnet.microsoft.com/download) must be installed, either directly or from an IDE (Visual Studio 2022/2026, Rider)

## Run the dll

```bash
# builds the .NET solution
dotnet build

# runs the console app
dotnet src/ConsoleApp/bin/Debug/net10.0/almops.dll --help
```

## Run the tests

Tests run on xunit v3 and the Microsoft Testing Platform (`global.json`).

```bash
# runs the tests that need no credential, exit code 8 meaning a project with no test left to run
dotnet test -- --filter-not-trait "Environment=Sandbox" --ignore-exit-code 8

# runs every test, the sandbox ones reading the AlmOps__Sandbox__* variables listed below
dotnet test
```

## Run from an IDE

* Create at the root of the repository a file `Local.runsettings` with the following content:

```xml
<?xml version="1.0" encoding="utf-8"?>
<RunSettings>
  <RunConfiguration>
    <EnvironmentVariables>
      <AlmOps__Sandbox__Organization>xxx</AlmOps__Sandbox__Organization>
      <AlmOps__Sandbox__Username>xxx</AlmOps__Sandbox__Username>
      <AlmOps__Sandbox__Token>xxx</AlmOps__Sandbox__Token>
      <AlmOps__Sandbox__Project>xxx</AlmOps__Sandbox__Project>
      <AlmOps__Sandbox__VariableGroupId>xxx</AlmOps__Sandbox__VariableGroupId>
      <AlmOps__Sandbox__GitLab__Token>glpat-xxxx</AlmOps__Sandbox__GitLab__Token>
      <AlmOps__Sandbox__GitLab__GroupId>xxx</AlmOps__Sandbox__GitLab__GroupId>
      <AlmOps__Sandbox__GitLab__ProjectId>xxx</AlmOps__Sandbox__GitLab__ProjectId>
    </EnvironmentVariables>
  </RunConfiguration>
</RunSettings>
```

* Configure IDE to use this file for test configuration
  * Rider: **File | Settings | Build, Execution, Deployment | Unit Testing | Test Runner**, then in `Test Settings` (at the bottom)

## Run from a NuGet package installation

```bash
# packs the projects
dotnet pack

# installs from local package
dotnet tool update -g almops --add-source=src/ConsoleApp/nupkg --version 1.3.0-alpha-000000

# adds .NET local tools to PATH
export PATH="$PATH:$HOME/.dotnet/tools"

# runs the tool
almops list projects

# uninstalls the tool
dotnet tool uninstall -g almops
```

## Run in a container

```bash
docker build . -t devprofr/almops -f src/ConsoleApp/Dockerfile --no-cache
docker run -it --rm --name almops \
  -e almops__BaseUrl="https://dev.azure.com/xxx" -e almops__Username="xxx" -e almops__Token="xxx" \
  devprofr/almops list projects
```

## Run the pipeline locally

[IstarCI](https://github.com/devpro/istarci) runs the GitHub Actions pipeline in Docker on every commit, and gates the push on its result.
`istarci add` and `istarci install-hook` set it up for this repository.

## References

### Azure DevOps documentation

* [Azure DevOps Services REST API Reference](https://learn.microsoft.com/en-us/rest/api/azure/devops/)
* [microsoft/azure-devops-auth-samples](https://github.com/microsoft/azure-devops-auth-samples)
* [microsoft/azure-devops-node-api](https://github.com/microsoft/azure-devops-node-api)
