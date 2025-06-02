using Newtonsoft.Json;

namespace AlmOps.GitLabComponent.Infrastructure.RestApi.Dto;

public class AuditEventDto
{
    public long Id { get; set; }
    
    [JsonProperty("author_id")]
    public long AuthorId { get; set; }
    
    [JsonProperty("entity_id")]
    public long EntityId { get; set; }
    
    [JsonProperty("entity_type")]
    public required string EntityType { get; set; }
    
    public required DetailsDto Details { get; set; }
    
    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [JsonProperty("ip_address")]
    public string? IpAddress { get; set; }
    
    [JsonProperty("author_name")]
    public string? AuthorName { get; set; }
    
    [JsonProperty("entity_path")]
    public string? EntityPath { get; set; }
    
    [JsonProperty("target_details")]
    public string? TargetDetails { get; set; }
    
    [JsonProperty("target_type")]
    public string? TargetType { get; set; }
    
    [JsonProperty("target_id")]
    public long TargetId { get; set; }
    
    [JsonProperty("event_type")]
    public string? EventType { get; set; }
}
