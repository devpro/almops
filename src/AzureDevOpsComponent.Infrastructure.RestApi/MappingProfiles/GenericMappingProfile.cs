﻿using AutoMapper;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.MappingProfiles
{
    public class GenericMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "AzureDevOpsRestApiGenericMappingProfile"; }
        }

        public GenericMappingProfile()
        {
            CreateMap<Dto.BuildDefinitionDto, Domain.Models.BuildDefinitionModel>();
            CreateMap<Domain.Models.BuildDefinitionModel, Dto.BuildDefinitionDto>()
                .ForMember(x => x.Drafts, opt => opt.Ignore());

            CreateMap<Dto.BuildArtifactDto, Domain.Models.BuildArtifactModel>()
                .ForMember(x => x.ResourceArtifactSize, opt => opt.MapFrom(x => x.Resource.Properties.ArtifactSize))
                .ForMember(x => x.ResourceData, opt => opt.MapFrom(x => x.Resource.Data))
                .ForMember(x => x.ResourceDownloadUrl, opt => opt.MapFrom(x => x.Resource.DownloadUrl))
                .ForMember(x => x.ResourceLocalPath, opt => opt.MapFrom(x => x.Resource.Properties.LocalPath))
                .ForMember(x => x.ResourceType, opt => opt.MapFrom(x => x.Resource.Type))
                .ForMember(x => x.ResourceUrl, opt => opt.MapFrom(x => x.Resource.Url));
            CreateMap<Domain.Models.BuildArtifactModel, Dto.BuildArtifactDto>()
                .ForMember(x => x.Resource, opt => opt.MapFrom(x => x));
            CreateMap<Domain.Models.BuildArtifactModel, Dto.ResourceDto>()
                .ForMember(x => x.Data, opt => opt.MapFrom(x => x.ResourceData))
                .ForMember(x => x.DownloadUrl, opt => opt.MapFrom(x => x.ResourceDownloadUrl))
                .ForMember(x => x.Properties, opt => opt.MapFrom(x => x))
                .ForMember(x => x.Type, opt => opt.MapFrom(x => x.ResourceType))
                .ForMember(x => x.Url, opt => opt.MapFrom(x => x.ResourceUrl));
            CreateMap<Domain.Models.BuildArtifactModel, Dto.ResourcePropertiesDto>()
                .ForMember(x => x.ArtifactSize, opt => opt.MapFrom(x => x.ResourceArtifactSize))
                .ForMember(x => x.LocalPath, opt => opt.MapFrom(x => x.ResourceLocalPath));

            CreateMap<Dto.BuildDto, Domain.Models.BuildModel>();
            CreateMap<Domain.Models.BuildModel, Dto.BuildDto>()
                .ForMember(x => x.Tags, opt => opt.Ignore())
                .ForMember(x => x.Repository, opt => opt.Ignore())
                .ForMember(x => x.Links, opt => opt.Ignore())
                .ForMember(x => x.Properties, opt => opt.Ignore())
                .ForMember(x => x.ValidationResults, opt => opt.Ignore())
                .ForMember(x => x.Plans, opt => opt.Ignore())
                .ForMember(x => x.TriggerInfo, opt => opt.Ignore())
                .ForMember(x => x.BuildNumberRevision, opt => opt.Ignore())
                .ForMember(x => x.Parameters, opt => opt.Ignore())
                .ForMember(x => x.OrchestrationPlan, opt => opt.Ignore())
                .ForMember(x => x.Logs, opt => opt.Ignore())
                .ForMember(x => x.KeepForever, opt => opt.Ignore())
                .ForMember(x => x.RetainedByRelease, opt => opt.Ignore())
                .ForMember(x => x.TriggeredByBuild, opt => opt.Ignore());

            CreateMap<Dto.PersonDto, Domain.Models.PersonModel>();
            CreateMap<Domain.Models.PersonModel, Dto.PersonDto>();

            CreateMap<Dto.PoolDto, Domain.Models.PoolModel>();
            CreateMap<Domain.Models.PoolModel, Dto.PoolDto>();

            CreateMap<Dto.ProjectDto, Domain.Models.ProjectModel>();
            CreateMap<Domain.Models.ProjectModel, Dto.ProjectDto>()
                .ForMember(x => x.LastUpdateTime, opt => opt.Ignore());

            CreateMap<Dto.QueueDto, Domain.Models.QueueModel>();
            CreateMap<Domain.Models.QueueModel, Dto.QueueDto>();

            CreateMap<Dto.ReleaseDto, Domain.Models.ReleaseModel>();
            CreateMap<Domain.Models.ReleaseModel, Dto.ReleaseDto>()
                .ForMember(x => x.Artifacts, opt => opt.Ignore())
                .ForMember(x => x.Links, opt => opt.Ignore());

            CreateMap<Dto.ReleaseDefinitionDto, Domain.Models.ReleaseDefinitionModel>();
            CreateMap<Domain.Models.ReleaseDefinitionModel, Dto.ReleaseDefinitionDto>()
                .ForMember(x => x.Links, opt => opt.Ignore())
                .ForMember(x => x.ProjectReference, opt => opt.Ignore())
                .ForMember(x => x.Properties, opt => opt.Ignore())
                .ForMember(x => x.Triggers, opt => opt.Ignore())
                .ForMember(x => x.Tags, opt => opt.Ignore())
                .ForMember(x => x.VariableGroups, opt => opt.Ignore())
                .ForMember(x => x.Variables, opt => opt.Ignore());

            CreateMap<Dto.EnvironmentDto, Domain.Models.EnvironmentModel>();
            CreateMap<Domain.Models.EnvironmentModel, Dto.EnvironmentDto>()
                .ForMember(x => x.DeploySteps, opt => opt.Ignore())
                .ForMember(x => x.DeployPhasesSnapshot, opt => opt.Ignore())
                .ForMember(x => x.WorkflowTasks, opt => opt.Ignore())
                .ForMember(x => x.Conditions, opt => opt.Ignore())
                .ForMember(x => x.Demands, opt => opt.Ignore())
                .ForMember(x => x.EnvironmentOptions, opt => opt.Ignore())
                .ForMember(x => x.PostApprovalsSnapshot, opt => opt.Ignore())
                .ForMember(x => x.PreApprovalsSnapshot, opt => opt.Ignore())
                .ForMember(x => x.PreDeployApprovals, opt => opt.Ignore())
                .ForMember(x => x.PostDeployApprovals, opt => opt.Ignore());

            CreateMap<Dto.ReleaseArtifactDto, Domain.Models.ReleaseArtifactModel>()
                .ForMember(x => x.BuildDefinitionId, opt => opt.MapFrom(x => x.DefinitionReference.Definition.Id))
                .ForMember(x => x.BuildDefinitionName, opt => opt.MapFrom(x => x.DefinitionReference.Definition.Name));
            CreateMap<Domain.Models.ReleaseArtifactModel, Dto.ReleaseArtifactDto>()
                .ForMember(x => x.DefinitionReference, opt => opt.MapFrom(x => x));
            CreateMap<Domain.Models.ReleaseArtifactModel, Dto.DefinitionReferenceDto>()
                .ForMember(x => x.ArtifactSourceDefinitionUrl, opt => opt.Ignore())
                .ForMember(x => x.ArtifactSourceVersionUrl, opt => opt.Ignore())
                .ForMember(x => x.DefaultVersionBranch, opt => opt.Ignore())
                .ForMember(x => x.DefaultVersionSpecific, opt => opt.Ignore())
                .ForMember(x => x.DefaultVersionTags, opt => opt.Ignore())
                .ForMember(x => x.DefaultVersionType, opt => opt.Ignore())
                .ForMember(x => x.Definition, opt => opt.MapFrom(x => new Dto.NameValueDto { Id = x.BuildDefinitionId, Name = x.BuildDefinitionName }))
                .ForMember(x => x.Project, opt => opt.Ignore())
                .ForMember(x => x.Version, opt => opt.Ignore());

            CreateMap<Dto.VariableGroupDto, Domain.Models.VariableGroupModel>();
        }
    }
}
