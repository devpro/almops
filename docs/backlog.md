# Backlog

Known debt and gaps, not yet scheduled.

## Verification against live APIs

The sandbox tests need credentials, so the move to System.Text.Json is proven against recorded JSON only.

- [ ] Run the sandbox tests (`AlmOps__Sandbox__*` variables) against Azure DevOps and GitLab, which no local run does.
- [ ] Check that Azure DevOps accepts the camelCase request bodies of `QueueBuildTask`, `CreateReleaseTask` and `UpdateVariableTask`, where Newtonsoft sent PascalCase.
- [ ] Check that a variable group update keeps the secret variables and the provider data, now that the group goes through `JsonObject` and the DTO.
- [ ] Check that every GitLab audit event carries `entity_type` and `details`, which `AuditEventDto` marks `required`, and which System.Text.Json enforces.
- [ ] Look for other fields the APIs send as a type the DTOs do not declare, as with the numeric build `id` read into a `string`:
  `LenientStringConverter` covers a number or a boolean read as text, nothing else.

## GitHub setup

- [ ] Set the repository variables `SONAR_ORG`, `SONAR_PROJECT_KEY`, `CONTAINER_REGISTRY_PATH` and `CONTAINER_IMAGE_NAME`.
  The SonarCloud project key was `devpro.almops`, which the README badge still reads.
- [ ] Set the secrets `SONAR_TOKEN`, `FOSSA_API_KEY`, `NUGET_API_KEY`, `DOCKERHUB_USERNAME`, `DOCKERHUB_TOKEN`, and the sandbox ones,
  `AZUREDEVOPS_TEST_ORG`, `AZUREDEVOPS_TEST_USERNAME`, `AZUREDEVOPS_TEST_TOKEN`, `AZUREDEVOPS_TEST_PROJECT`, `AZUREDEVOPS_TEST_VARIABLEGROUPID`,
  `GITLAB_TEST_TOKEN`, `GITLAB_TEST_GROUPID` and `GITLAB_TEST_PROJECTID`.
- [ ] Decide what a pull request from a fork does with the sandbox tests, which fail without the secrets.
- [ ] Archive the GitLab project (`devpro-labs/software/almops`) and the Azure DevOps pipeline definitions, whose pipeline files are gone.

## Packaging

- [ ] Decide whether the libraries are published to NuGet at all, since the `almops` tool package carries its own dependencies.
  `dotnet pack` publishes every packable project, so `AlmOps.Common.Infrastructure.RestApi` and `AlmOps.GitLabComponent.Infrastructure.RestApi` would appear on NuGet with the next version.
- [ ] Build the image on `bci/dotnet-runtime` rather than `bci/dotnet-aspnet`, a console application needing no ASP.NET Core, which would shrink the image and its scan findings.
- [ ] Bump `VersionPrefix` in `Directory.Build.props`, which gates the NuGet publication, when this branch is released.

## Code quality

- [ ] Fix the 21 remaining nullable warnings, in the console tasks (`CreateReleaseTask`, `QueueBuildTask`, `UpdateVariableTask`, `TaskBase`),
  `IBuildRepository` optional parameters, `AzureDevOpsRestApiConfiguration` and the tests, then consider `TreatWarningsAsErrors`.
- [ ] Remove the DTOs and DTO members no mapping or request uses, kept from the AutoMapper profile, or decide they document the API.
- [ ] Wire the GitLab component into the console application, or document it as a library only: no command uses it.

## Tests

- [ ] Check that the `Local.runsettings` of `CONTRIBUTING.md` still reaches the sandbox tests from Rider on the Microsoft Testing Platform,
  where `dotnet test --settings` runs none.
- [ ] Cover the console tasks with unit tests against mocked repositories, beyond the factory and `TaskBase`.

## Upstream

- [ ] Report to Withywoods that `HttpRepositoryBase` 2.x deserializes case sensitively, which leaves the camelCase fields of most REST APIs empty.
