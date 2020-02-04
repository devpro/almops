using AutoMapper;

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
            CreateMap<Dto.BuildDto, Domain.Models.BuildModel>();
            CreateMap<Domain.Models.BuildModel, Dto.BuildDto>();

            CreateMap<Dto.ProjectDto, Domain.Models.ProjectModel>();
            CreateMap<Domain.Models.ProjectModel, Dto.ProjectDto>()
                .ForMember(x => x.LastUpdateTime, opt => opt.Ignore());
        }
    }
}
