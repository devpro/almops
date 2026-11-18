using System.Collections.Generic;
using AlmOps.AzureDevOpsComponent.Domain.Models;
using AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Dto;
using Riok.Mapperly.Abstractions;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Mappers;

/// <summary>
/// Maps the DTOs of the Azure DevOps REST API to the domain models.
/// </summary>
/// <remarks>
/// A model keeps a subset of what the API returns, so every member of the model must be mapped, not every member of the DTO.
/// </remarks>
[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class AzureDevOpsDtoMapper
{
    [MapProperty("Resource.Type", nameof(BuildArtifactModel.ResourceType))]
    [MapProperty("Resource.Data", nameof(BuildArtifactModel.ResourceData))]
    [MapProperty("Resource.Url", nameof(BuildArtifactModel.ResourceUrl))]
    [MapProperty("Resource.DownloadUrl", nameof(BuildArtifactModel.ResourceDownloadUrl))]
    [MapProperty("Resource.Properties.LocalPath", nameof(BuildArtifactModel.ResourceLocalPath))]
    [MapProperty("Resource.Properties.ArtifactSize", nameof(BuildArtifactModel.ResourceArtifactSize))]
    public partial BuildArtifactModel ToModel(BuildArtifactDto dto);

    public partial List<BuildArtifactModel> ToModels(List<BuildArtifactDto> dtos);

    public partial BuildModel ToModel(BuildDto dto);

    public partial List<BuildModel> ToModels(List<BuildDto> dtos);

    public partial ProjectModel ToModel(ProjectDto dto);

    public partial List<ProjectModel> ToModels(List<ProjectDto> dtos);

    public partial ReleaseModel ToModel(ReleaseDto dto);

    public partial ReleaseDefinitionModel ToModel(ReleaseDefinitionDto dto);

    public partial List<ReleaseDefinitionModel> ToModels(List<ReleaseDefinitionDto> dtos);

    public partial VariableGroupModel ToModel(VariableGroupDto dto);

    [MapProperty("DefinitionReference.Definition.Id", nameof(ReleaseArtifactModel.BuildDefinitionId))]
    [MapProperty("DefinitionReference.Definition.Name", nameof(ReleaseArtifactModel.BuildDefinitionName))]
    private partial ReleaseArtifactModel ToModel(ReleaseArtifactDto dto);
}
