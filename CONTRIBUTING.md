# Contributing

## Requirements

* [.NET Core SDK](https://dotnet.microsoft.com/download) must be installed, either directly or with Visual Studio 2022

## Local project debug

```bash
# builds the .NET solution
dotnet build

# runs the console app
dotnet src/ConsoleApp/bin/Debug/net7.0/almops.dll --help
```

## Local installation

```bash
# packs the projects
dotnet pack

# installs from local package
dotnet tool update -g almops --add-source=src/ConsoleApp/nupkg --version 1.1.0-alpha-000000

# runs the tool
almops list projects

# uninstalls the tool
dotnet tool uninstall -g almops
```

## References

### Microsoft documentation

* [Azure DevOps Services REST API Reference](https://docs.microsoft.com/en-us/rest/api/azure/devops/)
* [microsoft/azure-devops-auth-samples](https://github.com/microsoft/azure-devops-auth-samples)
* [microsoft/azure-devops-node-api](https://github.com/microsoft/azure-devops-node-api)
