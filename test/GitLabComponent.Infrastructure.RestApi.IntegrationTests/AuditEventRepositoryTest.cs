using AlmOps.Common.System;
using AlmOps.GitLabComponent.Infrastructure.RestApi.Repositories;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace AlmOps.GitLabComponent.Infrastructure.RestApi.IntegrationTests;

[Trait("Environment", EnvironmentName)]
public class AuditEventRepositoryTest() : RepositoryIntegrationTestBase(EnvironmentName)
{
    private const string EnvironmentName = "Sandbox";

    [Fact]
    public async Task AuditEventRepositoryFindAtGroupLevelAsync_OnSandbox_ReturnNotEmptyList()
    {
        // Arrange
        var repository = ServiceProvider.GetRequiredService<AuditEventRepository>();
        var groupId = EnvironmentExtensions.GetEnvironmentVariable(EnvironmentName, "GitLab", "GroupId");

        // Act
        var output = await repository.FindAtGroupLevelAsync(groupId);

        // Assert
        output.Should().NotBeNull();
        output.Should().NotBeEmpty();
    }

    [Fact]
    public async Task AuditEventRepositoryFindAtProjectLevelAsync_OnSandbox_ReturnNotEmptyList()
    {
        // Arrange
        var repository = ServiceProvider.GetRequiredService<AuditEventRepository>();
        var projectId = EnvironmentExtensions.GetEnvironmentVariable(EnvironmentName, "GitLab", "ProjectId");

        // Act
        var output = await repository.FindAtProjectLevelAsync(projectId);

        // Assert
        output.Should().NotBeNull();
        output.Should().NotBeEmpty();
    }
}
