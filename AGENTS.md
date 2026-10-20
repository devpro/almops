# AGENTS.md

Guidance for coding agents working in this repository.

## Project overview

almops is a command line tool to administrate ALM solutions: Azure DevOps through its REST API, and GitLab audit events.
It is .NET 10 / C#, shipped as the `almops` .NET global tool on NuGet and as the `devprofr/almops` container image.

Project                                   | Role
------------------------------------------|----------------------------------------------------------------
`src/ConsoleApp`                          | Command line parsing, configuration and one task per action
`src/AzureDevOpsComponent.Domain`         | Models and repository interfaces
`src/*Component.Infrastructure.RestApi`   | REST API clients, DTOs and the Mapperly mapper from DTO to model
`samples/SamlTokenClaimEnricher`          | Independent Azure Function sample

## Working rules

**Every command runs in the foreground, and the agent waits for it.**
No background command, no subagent, no fork, no parallel task, even for a long command.

**Linters for Markdown and YAML are never run by an agent, and never imitated by hand.**
An agent that notices text a linter might flag says so in its report and changes nothing.
`dotnet format --verify-no-changes --severity warn`, which CI runs on the C# code, is a check and may be run.

**Only GitHub Actions published by `github` or `docker` are allowed in workflows.**
Reusable workflows and actions of `devpro/github-workflow-parts` are also allowed.

**The CI is the GitHub Actions pipeline, which IstarCI runs locally on every commit.**
`istarci status .` shows the last run and `istarci logs . --job <name>` the log of a job.
`.istarci.yml` gives a local run the repository variables it would otherwise get as placeholders.

**Test before code when applicable.**
A regression test is seen failing for the stated reason before the fix.

**The machine is left as it was found, apart from the intended changes.**

**Preserve existing comments and formatting when editing a file.**
Commit only when asked, and never push.

**Documentation is as short as possible, and headers stay.**

## Writing style

Applies to Markdown, code comments, commit messages and prose in scripts.

- **A comment says why, not what, and the why is timeless.**
  It records the failure it prevents or the constraint that made the obvious shape wrong, never the story of the change.
- **One thought per line.**
  Every sentence starts on its own line, and a long one breaks at a clause boundary, never to fit a width.
- **No em dash, no en dash.**
  A colon, a comma, or a full stop.
- **No second person.**
  "The working tree", not "your working tree"; `<token>`, not `<your-token>`.

## Conventions

- NuGet versions are managed centrally in `Directory.Packages.props`: `PackageReference` entries in csproj files have no `Version` attribute.
- Release version is `VersionPrefix` in `Directory.Build.props`.
- No package with a license issue: Mapperly maps, never AutoMapper; AwesomeAssertions asserts, never FluentAssertions.
- Tests use xunit v3 on the Microsoft Testing Platform (`global.json`), so runner options go after `--` with `dotnet test --project`.
- A test reaching a live Azure DevOps or GitLab sandbox carries `[Trait("Environment", "Sandbox")]` and reads its credentials from `AlmOps__Sandbox__*` environment variables.
- C# style is enforced by `.editorconfig`.
- Shell scripts are named in `snake_case` and committed with the executable bit set (`git update-index --chmod=+x`).
- Target platform is Linux, including WSL2, and `bash`.

## Commands

```bash
dotnet build
dotnet test -- --filter-not-trait "Environment=Sandbox" --ignore-exit-code 8
dotnet run --project src/ConsoleApp -- list projects
```

Exit code 8 means a test project ran no test, which every integration project does without the sandbox credentials of [CONTRIBUTING.md](CONTRIBUTING.md).
