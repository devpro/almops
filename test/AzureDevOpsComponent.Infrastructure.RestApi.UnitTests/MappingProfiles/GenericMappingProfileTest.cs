using AutoMapper;
using Xunit;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.UnitTests.MappingProfiles;

[Trait("Category", "UnitTests")]
public class GenericMappingProfileTest
{
    [Fact]
    public void GenericMappingProfileBuildAutoMapper_AssertConfigurationIsValid()
    {
        var mappingConfig = new MapperConfiguration(x =>
        {
            x.AddProfile(new RestApi.MappingProfiles.GenericMappingProfile());
            x.AllowNullCollections = true;
        });
        var mapper = mappingConfig.CreateMapper();
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}
