using AlmOps.GitLabComponent.Infrastructure.RestApi.Dto;
using Microsoft.Extensions.Logging;

namespace AlmOps.GitLabComponent.Infrastructure.RestApi.Repositories;

/// <summary>
/// Repository to retrieve GitLab audit events.
/// </summary>
/// <param name="configuration"><see cref="GitLabRestApiConfiguration"/></param>
/// <param name="logger"><see cref="ILogger"/></param>
/// <param name="httpClientFactory"><see cref="IHttpClientFactory"/></param>
/// <remarks>
/// https://docs.gitlab.com/api/audit_events/
/// </remarks>
public class AuditEventRepository(GitLabRestApiConfiguration configuration, ILogger<AuditEventRepository> logger, IHttpClientFactory httpClientFactory)
    : RepositoryBase(configuration, logger, httpClientFactory)
{
    public async Task<List<AuditEventDto>> FindAtInstanceLevelAsync()
    {
        return await GetAsync<List<AuditEventDto>>(GenerateUrl("audit_events"));
    }
    
    public async Task<List<AuditEventDto>> FindAtGroupLevelAsync(string groupId)
    {
        return await GetAsync<List<AuditEventDto>>(GenerateUrl($"groups/{groupId}/audit_events"));
    }
    
    public async Task<List<AuditEventDto>> FindAtProjectLevelAsync(string projectId)
    {
        return await GetAsync<List<AuditEventDto>>(GenerateUrl($"projects/{projectId}/audit_events"));
    }
}
