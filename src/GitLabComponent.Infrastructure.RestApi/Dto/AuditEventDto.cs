using System;
using System.Text.Json.Serialization;

namespace AlmOps.GitLabComponent.Infrastructure.RestApi.Dto;

public class AuditEventDto
{
    public long Id { get; set; }

    [JsonPropertyName("author_id")]
    public long AuthorId { get; set; }

    [JsonPropertyName("entity_id")]
    public long EntityId { get; set; }

    [JsonPropertyName("entity_type")]
    public required string EntityType { get; set; }

    public required DetailsDto Details { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("ip_address")]
    public string? IpAddress { get; set; }

    [JsonPropertyName("author_name")]
    public string? AuthorName { get; set; }

    [JsonPropertyName("entity_path")]
    public string? EntityPath { get; set; }

    [JsonPropertyName("target_details")]
    public string? TargetDetails { get; set; }

    [JsonPropertyName("target_type")]
    public string? TargetType { get; set; }

    [JsonPropertyName("target_id")]
    public long TargetId { get; set; }

    [JsonPropertyName("event_type")]
    public string? EventType { get; set; }
}
