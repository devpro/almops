using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AlmOps.AzureDevOpsComponent.Domain.Models;
using AlmOps.AzureDevOpsComponent.Domain.Repositories;
using AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Dto;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Withywoods.Serialization.Json;

namespace AlmOps.AzureDevOpsComponent.Infrastructure.RestApi.Repositories;

/// <summary>
/// Build repository.
/// </summary>
/// <remarks>
/// https://learn.microsoft.com/en-us/rest/api/azure/devops/distributedtask/variablegroups
/// </remarks>
public class VariableGroupRepository(
    IAzureDevOpsRestApiConfiguration configuration,
    ILogger<VariableGroupRepository> logger,
    IHttpClientFactory httpClientFactory,
    IMapper mapper)
    : RepositoryBase(configuration, logger, httpClientFactory, mapper), IVariableGroupRepository
{
    protected override string ResourceName => "_apis/distributedtask/variablegroups";

    /// <summary>
    /// Find one variable group per id.
    /// </summary>
    /// <param name="projectName"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <remarks>
    /// https://docs.microsoft.com/en-us/rest/api/azure/devops/distributedtask/variablegroups/get
    /// </remarks>
    public async Task<VariableGroupModel> FindOneByIdAsync(string projectName, string id)
    {
        var result = await FindOneVariableGroupDto(projectName, id);
        return Mapper.Map<VariableGroupModel>(result);
    }

    /// <summary>
    /// Update one variable group.
    /// </summary>
    /// <param name="projectName"></param>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <param name="isReplaceAll"></param>
    /// <returns></returns>
    /// <remarks>
    /// https://learn.microsoft.com/en-us/rest/api/azure/devops/distributedtask/variablegroups/update
    /// </remarks>
    public async Task UpdateAsync(string projectName, string id, Dictionary<string, string> input, bool isReplaceAll = false)
    {
        var existing = await FindOneVariableGroupDto(projectName, id);
        if (existing == null)
        {
            throw new System.ArgumentNullException(nameof(id));
        }

        if (isReplaceAll)
        {
            existing.Variables = new JObject();
        }

        foreach (var variable in input)
        {
            if (existing.Variables[variable.Key] != null)
            {
                existing.Variables[variable.Key]["value"] = variable.Value;
                continue;
            }

            existing.Variables.Add(variable.Key, $"{{\"value\":\"{variable.Value}\"}}".FromJson<JToken>());
        }

        var url = GenerateUrl(prefix: $"/{projectName}", suffix: $"/{id}");
        await PutAsync(url, existing);
    }

    /// <summary>
    /// Find one variable group from its id.
    /// </summary>
    /// <param name="projectName">Project name</param>
    /// <param name="id">Variable group name</param>
    /// <returns></returns>
    /// <remarks>
    /// https://learn.microsoft.com/en-us/rest/api/azure/devops/distributedtask/variablegroups/get
    /// </remarks>
    private async Task<VariableGroupDto> FindOneVariableGroupDto(string projectName, string id)
    {
        var url = GenerateUrl(prefix: $"/{projectName}", suffix: $"/{id}");
        return await GetAsync<VariableGroupDto>(url);
    }
}
