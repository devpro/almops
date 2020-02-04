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
```

## References

### Microsoft documentation

* [Azure DevOps Services REST API Reference](https://docs.microsoft.com/en-us/rest/api/azure/devops/)
* [microsoft/azure-devops-auth-samples](https://github.com/microsoft/azure-devops-auth-samples)
