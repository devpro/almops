using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Dto;
using AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Mappers;
using AwesomeAssertions;
using Xunit;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.UnitTests.Mappers;

[Trait("Category", "UnitTests")]
public class AzureDevOpsDtoMapperTest
{
    private readonly AzureDevOpsDtoMapper _mapper = new();

    [Fact]
    public void ToModel_BuildArtifact_FlattensResource()
    {
        var dto = new BuildArtifactDto
        {
            Id = 12,
            Name = "drop",
            Source = "source-1",
            Resource = new ResourceDto
            {
                Type = "Container",
                Data = "#/1/drop",
                Url = "https://dev.azure.com/org/artifacts/12",
                DownloadUrl = "https://dev.azure.com/org/artifacts/12/zip",
                Properties = new ResourcePropertiesDto { LocalPath = "/drop", ArtifactSize = "1024" }
            }
        };

        var model = _mapper.ToModel(dto);

        model.Id.Should().Be(12);
        model.Name.Should().Be("drop");
        model.Source.Should().Be("source-1");
        model.ResourceType.Should().Be("Container");
        model.ResourceData.Should().Be("#/1/drop");
        model.ResourceUrl.Should().Be("https://dev.azure.com/org/artifacts/12");
        model.ResourceDownloadUrl.Should().Be("https://dev.azure.com/org/artifacts/12/zip");
        model.ResourceLocalPath.Should().Be("/drop");
        model.ResourceArtifactSize.Should().Be("1024");
    }

    [Fact]
    public void ToModel_BuildArtifactWithoutResource_LeavesResourceFieldsEmpty()
    {
        var model = _mapper.ToModel(new BuildArtifactDto { Id = 3, Name = "logs" });

        model.Name.Should().Be("logs");
        model.ResourceType.Should().BeNull();
        model.ResourceLocalPath.Should().BeNull();
    }

    [Fact]
    public void ToModel_Build_MapsNestedObjects()
    {
        var dto = new BuildDto
        {
            Id = "264",
            BuildNumber = "20261020.1",
            Status = "completed",
            Result = "succeeded",
            QueueTime = new DateTime(2026, 10, 20, 8, 0, 0, DateTimeKind.Utc),
            SourceBranch = "refs/heads/main",
            Definition = new BuildDefinitionDto { Id = "3", Name = "almops-ci", Project = new ProjectDto { Name = "almops" } },
            Project = new ProjectDto { Id = "p1", Name = "almops" },
            Queue = new QueueDto { Id = 9, Name = "Azure Pipelines", Pool = new PoolDto { Id = 1, Name = "Hosted", IsHosted = true } },
            RequestedFor = new PersonDto { DisplayName = "Build Agent", UniqueName = "agent@devpro.fr" },
            Tags = ["release"]
        };

        var model = _mapper.ToModel(dto);

        model.Id.Should().Be("264");
        model.BuildNumber.Should().Be("20261020.1");
        model.QueueTime.Should().Be(dto.QueueTime);
        model.SourceBranch.Should().Be("refs/heads/main");
        model.Definition!.Name.Should().Be("almops-ci");
        model.Definition.Project!.Name.Should().Be("almops");
        model.Project!.Id.Should().Be("p1");
        model.Queue!.Pool!.IsHosted.Should().BeTrue();
        model.RequestedFor!.UniqueName.Should().Be("agent@devpro.fr");
        model.RequestedBy.Should().BeNull();
    }

    [Fact]
    public void ToModel_Release_ConvertsIdAndMapsEnvironments()
    {
        var dto = new ReleaseDto
        {
            Id = 42,
            Name = "Release-42",
            Status = "active",
            Environments = [new EnvironmentDto { Id = 7, Name = "Production", Owner = new PersonDto { DisplayName = "Ops" } }],
            ReleaseDefinition = new ReleaseDefinitionDto { Id = 5, Name = "almops-cd" },
            ProjectReference = new ProjectDto { Name = "almops" }
        };

        var model = _mapper.ToModel(dto);

        model.Id.Should().Be("42");
        model.Name.Should().Be("Release-42");
        model.Environments.Should().ContainSingle().Which.Owner!.DisplayName.Should().Be("Ops");
        model.ReleaseDefinition!.Name.Should().Be("almops-cd");
        model.ProjectReference!.Name.Should().Be("almops");
    }

    [Fact]
    public void ToModel_ReleaseDefinition_FlattensArtifactDefinitionReference()
    {
        var dto = new ReleaseDefinitionDto
        {
            Id = 5,
            Name = "almops-cd",
            Artifacts =
            [
                new ReleaseArtifactDto
                {
                    Alias = "_almops-ci",
                    IsPrimary = true,
                    SourceId = "p1:3",
                    Type = "Build",
                    DefinitionReference = new DefinitionReferenceDto { Definition = new NameValueDto { Id = "3", Name = "almops-ci" } }
                },
                new ReleaseArtifactDto { Alias = "_other" }
            ]
        };

        var model = _mapper.ToModel(dto);

        model.Artifacts.Should().HaveCount(2);
        var primary = model.Artifacts![0];
        primary.Alias.Should().Be("_almops-ci");
        primary.IsPrimary.Should().BeTrue();
        primary.BuildDefinitionId.Should().Be("3");
        primary.BuildDefinitionName.Should().Be("almops-ci");
        model.Artifacts[1].BuildDefinitionId.Should().BeNull();
    }

    [Fact]
    public void ToModel_VariableGroup_KeepsVariables()
    {
        var variables = JsonNode.Parse("{\"key1\":{\"value\":\"value1\"}}")!.AsObject();
        var dto = new VariableGroupDto { Id = 8, Name = "almops-vars", Type = "Vsts", Variables = variables };

        var model = _mapper.ToModel(dto);

        model.Id.Should().Be(8);
        model.Name.Should().Be("almops-vars");
        model.Variables.Should().BeSameAs(variables);
    }

    [Fact]
    public void ToModels_Projects_MapsEveryItem()
    {
        List<ProjectDto> dtos = [new() { Id = "1", Name = "almops", Visibility = "public" }, new() { Id = "2", Name = "keeptrack" }];

        var models = _mapper.ToModels(dtos);

        models.Should().HaveCount(2);
        models[0].Visibility.Should().Be("public");
        models[1].Name.Should().Be("keeptrack");
    }
}
