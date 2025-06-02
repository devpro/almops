using Newtonsoft.Json;

namespace AlmOps.GitLabComponent.Infrastructure.RestApi.Dto;

public class DetailsDto
{
    [JsonProperty("custom_message")]
    public string? CustomMessage { get; set; }
    
    [JsonProperty("author_name")]
    public string? AuthorName { get; set; }
    
    [JsonProperty("author_email")]
    public string? AuthorEmail { get; set; }
    
    [JsonProperty("target_id")]
    public object? TargetId { get; set; }
    
    [JsonProperty("target_type")]
    public string? TargetType { get; set; }
    
    [JsonProperty("target_details")]
    public string? TargetDetails { get; set; }
    
    [JsonProperty("ip_address")]
    public string? IpAddress { get; set; }
    
    [JsonProperty("entity_path")]
    public string? EntityPath { get; set; }
    
    public string? Add { get; set; }
    
    public string? Change { get; set; }
    
    public string? From { get; set; }
    
    public string? To { get; set; }
    
    [JsonProperty("author_class")]
    public string? AuthorClass { get; set; }
}
